using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BezierCurveRoad : MonoBehaviour
{
    public Material lineMaterial; // �������� ��� ������������ ����� (���� �����)
    public GameObject pointPrefab; // ������ ��� ����� (��������, �����)
    public float roadWidth = 1.0f; // ������ ������
    public int segments = 100; // ���������� ��������� ��� ���������� ������

    private List<GameObject> pointObjects = new List<GameObject>(); // ������ �������� �����
    private Mesh roadMesh; // Mesh ��� ������
    private GameObject selectedPoint = null; // �����, ������� �������������

    private void Update()
    {
        // ���������� ����� ����� ��� ����� ����� ������� ����
        if (Input.GetMouseButtonDown(0) && !IsPointerOverPoint())
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // ��������� �� ������
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // ������ ����� ������ �����
            GameObject newPoint = Instantiate(pointPrefab, worldPos, Quaternion.identity);
            pointObjects.Add(newPoint);

            // ������������� Mesh
            GenerateRoadMesh();
        }

        // ����������� �����, ���� ��� �������
        if (selectedPoint != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // ��������� �� ������
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            selectedPoint.transform.position = worldPos;

            // ������������� Mesh
            GenerateRoadMesh();
        }

        // ����� ����� ��� ����������� ��� ������� ������ ������� ����
        if (Input.GetMouseButtonDown(1))
        {
            selectedPoint = GetPointUnderMouse();
        }

        // ��������� ��������� �����
        if (Input.GetMouseButtonUp(1))
        {
            selectedPoint = null;
        }
    }

    // ������� ��� ��������, ��������� �� ���� ��� ������
    private bool IsPointerOverPoint()
    {
        return GetPointUnderMouse() != null;
    }

    // ������� ��� ���������� ����� ��� �����
    private GameObject GetPointUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (pointObjects.Contains(hit.collider.gameObject))
            {
                return hit.collider.gameObject;
            }
        }
        return null;
    }

    // ��������� Mesh ��� ������
    private void GenerateRoadMesh()
    {
        if (pointObjects.Count < 2)
            return; // ���������� ������� ��� �����

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < segments; i++)
        {
            float t1 = i / (float)segments;
            float t2 = (i + 1) / (float)segments;

            // ����� �� ������
            Vector3 point1 = CalculateBezierPoint(t1);
            Vector3 point2 = CalculateBezierPoint(t2);

            // ������� � ������ (�������������)
            Vector3 tangent = (point2 - point1).normalized;
            Vector3 normal = Vector3.Cross(tangent, Vector3.forward).normalized;

            // ��� ����� �� ����� �� ������ (��� ���������� ������)
            Vector3 left1 = point1 - normal * roadWidth * 0.5f;
            Vector3 right1 = point1 + normal * roadWidth * 0.5f;
            Vector3 left2 = point2 - normal * roadWidth * 0.5f;
            Vector3 right2 = point2 + normal * roadWidth * 0.5f;

            // ��������� �������
            vertices.Add(left1);
            vertices.Add(right1);
            vertices.Add(left2);
            vertices.Add(right2);

            // ��������� ������������
            int vertIndex = vertices.Count - 4;
            triangles.Add(vertIndex);
            triangles.Add(vertIndex + 1);
            triangles.Add(vertIndex + 2);

            triangles.Add(vertIndex + 1);
            triangles.Add(vertIndex + 3);
            triangles.Add(vertIndex + 2);
        }

        // ������� ����� Mesh
        if (roadMesh == null)
        {
            roadMesh = new Mesh();
            GetComponent<MeshFilter>().mesh = roadMesh;
        }
        roadMesh.Clear();
        roadMesh.vertices = vertices.ToArray();
        roadMesh.triangles = triangles.ToArray();
        roadMesh.RecalculateNormals();
    }

    // ������� ��� ������� ����� �� ������ �����
    Vector3 CalculateBezierPoint(float t)
    {
        List<Vector3> controlPoints = new List<Vector3>();
        foreach (GameObject pointObject in pointObjects)
        {
            controlPoints.Add(pointObject.transform.position);
        }

        List<Vector3> temp = new List<Vector3>(controlPoints);

        while (temp.Count > 1)
        {
            List<Vector3> next = new List<Vector3>();
            for (int i = 0; i < temp.Count - 1; i++)
            {
                Vector3 point = Vector3.Lerp(temp[i], temp[i + 1], t);
                next.Add(point);
            }
            temp = next;
        }

        return temp[0];
    }
}
