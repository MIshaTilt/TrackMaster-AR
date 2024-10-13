using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class BezierCurveRoad : MonoBehaviour
{
    public Material lineMaterial; // Материал для визуализации линии (если нужно)
    public GameObject pointPrefab; // Префаб для точек (например, сфера)
    public float roadWidth = 1.0f; // Ширина дороги
    public int segments = 100; // Количество сегментов для прорисовки кривой

    private List<GameObject> pointObjects = new List<GameObject>(); // Список объектов точек
    private Mesh roadMesh; // Mesh для дороги
    private GameObject selectedPoint = null; // Точка, которую перетаскивают

    private void Update()
    {
        // Добавление новой точки при клике левой кнопкой мыши
        if (Input.GetMouseButtonDown(0) && !IsPointerOverPoint())
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // Дистанция от камеры
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            // Создаём новый объект точки
            GameObject newPoint = Instantiate(pointPrefab, worldPos, Quaternion.identity);
            pointObjects.Add(newPoint);

            // Перестраиваем Mesh
            GenerateRoadMesh();
        }

        // Перемещение точки, если она выбрана
        if (selectedPoint != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; // Дистанция от камеры
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            selectedPoint.transform.position = worldPos;

            // Перестраиваем Mesh
            GenerateRoadMesh();
        }

        // Выбор точки для перемещения при нажатии правой кнопкой мыши
        if (Input.GetMouseButtonDown(1))
        {
            selectedPoint = GetPointUnderMouse();
        }

        // Отпустить выбранную точку
        if (Input.GetMouseButtonUp(1))
        {
            selectedPoint = null;
        }
    }

    // Функция для проверки, находится ли мышь над точкой
    private bool IsPointerOverPoint()
    {
        return GetPointUnderMouse() != null;
    }

    // Функция для нахождения точки под мышью
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

    // Генерация Mesh для дороги
    private void GenerateRoadMesh()
    {
        if (pointObjects.Count < 2)
            return; // Необходимо минимум две точки

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        for (int i = 0; i < segments; i++)
        {
            float t1 = i / (float)segments;
            float t2 = (i + 1) / (float)segments;

            // Точки на кривой
            Vector3 point1 = CalculateBezierPoint(t1);
            Vector3 point2 = CalculateBezierPoint(t2);

            // Нормаль к кривой (перпендикуляр)
            Vector3 tangent = (point2 - point1).normalized;
            Vector3 normal = Vector3.Cross(tangent, Vector3.forward).normalized;

            // Две точки по бокам от кривой (для построения дороги)
            Vector3 left1 = point1 - normal * roadWidth * 0.5f;
            Vector3 right1 = point1 + normal * roadWidth * 0.5f;
            Vector3 left2 = point2 - normal * roadWidth * 0.5f;
            Vector3 right2 = point2 + normal * roadWidth * 0.5f;

            // Добавляем вершины
            vertices.Add(left1);
            vertices.Add(right1);
            vertices.Add(left2);
            vertices.Add(right2);

            // Добавляем треугольники
            int vertIndex = vertices.Count - 4;
            triangles.Add(vertIndex);
            triangles.Add(vertIndex + 1);
            triangles.Add(vertIndex + 2);

            triangles.Add(vertIndex + 1);
            triangles.Add(vertIndex + 3);
            triangles.Add(vertIndex + 2);
        }

        // Создаем новый Mesh
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

    // Функция для расчета точки на кривой Безье
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
