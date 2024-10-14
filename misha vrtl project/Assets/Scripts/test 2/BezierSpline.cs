using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BezierSpline : MonoBehaviour
{
    public List<Transform> controlPoints = new List<Transform>(); // ������ ���� ����������� � ������� �����

    public LineRenderer lineRenderer;  // LineRenderer ��� ��������� �����

    public int resolution = 50;

    private void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;  // ���������� �������������, ��� ��� ����� ��� ����������
    }

    private void Update()
    {
        DrawBezierCurve();
        /*if (Input.GetMouseButtonDown(1)) // ����� ������ ���� ��� ���������� �������� ����������� �����
        {
            lineRenderer.positionCount = 2;
            Vector3[] points = new Vector3[2]; // ������� ������ �� ������ ��������
            points[0] = new Vector3(1, 2, 3);  // ������� ����� � ������������ (1, 2, 3)
            points[1] = new Vector3(1, 3, 3);  // ������� ����� � ������������ (1, 2, 3)

            lineRenderer.SetPositions(points);
        }*/
    }

    // ������� ��� ��������� ������
    private void DrawBezierCurve()
    {
        if (controlPoints.Count < 4) // �� �������� ��������, ���� ������ 4 ����� (��������� ������� ��� ����� ������ �����)
            return;

        List<Vector3> curvePoints = new List<Vector3>();
 
        for (int i = 0; i < controlPoints.Count-3; i += 3)
        {
            for (int j = 0; j <= resolution; j++)
            {
                float t = j / (float)resolution;
                Vector3 point = CalculateBezierPoint(t, controlPoints[i].position, controlPoints[i + 1].position, controlPoints[i + 2].position, controlPoints[i + 3].position);
                curvePoints.Add(point);
                //Debug.Log(point);


            }
        }




        /*for (int i = 1; i < controlPoints.Count; i += 3)
        {
            for (int j = 0; j <= resolution; j++)
            {
                float t = j / (float)resolution;
                Vector3 point = CalculateBezierPoint(t, controlPoints[i].position, controlPoints[i + 4].position, controlPoints[i + 5].position, controlPoints[i + 3].position);
                curvePoints.Add(point);
                //Debug.Log($"{t}{point}");
            }
        }*/

        // ������������� ���������� ����� ��� LineRenderer
        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
    }

    /*private void OnDrawGizmos()
    {
        if (controlPoints.Count < 4) // ������ ���� ������� 4 ����� ��� ���������� �������� �����
            return;

        Gizmos.color = Color.red;

        // �������� �� ������ �������� ����� ��� ���������� ���������
        for (int i = 0; i < 3; i += 3)
        {
            for (float t = 0; t <= 1; t += 0.05f)
            {
                Vector3 point = CalculateBezierPoint(t, controlPoints[i].position, controlPoints[i + 2].position, controlPoints[i + 3].position, controlPoints[i + 1].position);
                Gizmos.DrawSphere(point, 0.1f);
                //Debug.Log($"{i},{i + 1},{i + 2},{i + 3}");

            }
        }
        for (int i = 1; i < controlPoints.Count; i += 3)
        {
            for (float t = 0; t <= 1; t += 0.05f)
            {
                Vector3 point = CalculateBezierPoint(t, controlPoints[i].position, controlPoints[i + 4].position, controlPoints[i + 5].position, controlPoints[i + 3].position);
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }*/

    // ������ ����� �� ������ ����� � �������� �������� �������
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // ��������� �����
        p += 3 * uu * t * p1; // ������ ����������� �����
        p += 3 * u * tt * p2; // ������ ����������� �����
        p += ttt * p3; // �������� �����

        return p;
    }


}
