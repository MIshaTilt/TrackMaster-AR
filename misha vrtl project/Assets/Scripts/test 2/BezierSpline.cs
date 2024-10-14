using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BezierSpline : MonoBehaviour
{
    public List<Transform> controlPoints = new List<Transform>(); // Список всех контрольных и опорных точек

    public LineRenderer lineRenderer;  // LineRenderer для рисования линии

    public int resolution = 50;

    private void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;  // Изначально устанавливаем, что нет точек для рендеринга
    }

    private void Update()
    {
        DrawBezierCurve();
        /*if (Input.GetMouseButtonDown(1)) // Левая кнопка мыши для добавления основной контрольной точки
        {
            lineRenderer.positionCount = 2;
            Vector3[] points = new Vector3[2]; // Создаем массив из одного элемента
            points[0] = new Vector3(1, 2, 3);  // Заносим точку с координатами (1, 2, 3)
            points[1] = new Vector3(1, 3, 3);  // Заносим точку с координатами (1, 2, 3)

            lineRenderer.SetPositions(points);
        }*/
    }

    // Функция для рисования кривой
    private void DrawBezierCurve()
    {
        if (controlPoints.Count < 4) // Не начинаем рисовать, если меньше 4 точек (требуется минимум для одной кривой Безье)
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

        // Устанавливаем количество точек для LineRenderer
        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
    }

    /*private void OnDrawGizmos()
    {
        if (controlPoints.Count < 4) // Должно быть минимум 4 точки для построения сегмента Безье
            return;

        Gizmos.color = Color.red;

        // Проходим по каждой четверке точек для построения сегментов
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

    // Расчет точки на кривой Безье с четырьмя опорными точками
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0; // Начальная точка
        p += 3 * uu * t * p1; // Первая контрольная точка
        p += 3 * u * tt * p2; // Вторая контрольная точка
        p += ttt * p3; // Конечная точка

        return p;
    }


}
