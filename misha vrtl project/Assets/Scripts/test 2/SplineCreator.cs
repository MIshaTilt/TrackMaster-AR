using System.Collections.Generic;
using UnityEngine;

public class SplineCreator : MonoBehaviour
{

        public GameObject controlPointPrefab;  // ѕрефаб дл€ основных контрольных точек
        public GameObject intermediatePointPrefab;  // ѕрефаб дл€ опорных (промежуточных) точек

        private BezierSpline spline;

        public List<bool> isControl = new List<bool>();

        void Start()
        {
            spline = GetComponent<BezierSpline>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Ћева€ кнопка мыши дл€ добавлени€ основной контрольной точки
            {
                Vector3 newPosition = GetMouseWorldPosition();
                AddNewControlPoint(newPosition);
            }
        }

        // ѕолучение позиции мыши в мировых координатах
        Vector3 GetMouseWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float distance;

            if (groundPlane.Raycast(ray, out distance))
            {
                return ray.GetPoint(distance);
            }

            return Vector3.zero;
        }

        // ƒобавление новой контрольной точки и автоматическое добавление опорных точек
        void AddNewControlPoint(Vector3 position)
        {
            // —оздаем основную контрольную точку с использованием первого префаба
            GameObject newControlPoint = Instantiate(controlPointPrefab, position, Quaternion.identity);
            spline.controlPoints.Add(newControlPoint.transform);
            isControl.Add(true);

            // ≈сли это не перва€ точка, добавл€ем промежуточные опорные точки
            if (spline.controlPoints.Count == 2)
            {
                // –асчет позиций дл€ опорных точек
                Transform previousPoint = spline.controlPoints[spline.controlPoints.Count - 2];
                Vector3 controlPoint1 = Vector3.Lerp(previousPoint.position, position, 0.33f);
                Vector3 controlPoint2 = Vector3.Lerp(previousPoint.position, position, 0.66f);

                // —оздаем и добавл€ем промежуточные опорные точки с использованием второго префаба
                AddIntermediateControlPoint(controlPoint1);
                AddIntermediateControlPoint(controlPoint2);
            //Debug.Log("1");
            }
            else if(spline.controlPoints.Count > 2)
            {
                // –асчет позиций дл€ опорных точек
                Transform previousPoint = spline.controlPoints[spline.controlPoints.Count - 4];
                Vector3 controlPoint1 = Vector3.Lerp(previousPoint.position, position, 0.33f);
                Vector3 controlPoint2 = Vector3.Lerp(previousPoint.position, position, 0.66f);

                // —оздаем и добавл€ем промежуточные опорные точки с использованием второго префаба
                AddIntermediateControlPoint(controlPoint1);
                AddIntermediateControlPoint(controlPoint2);
            //Debug.Log("2");
        }
    }

        // ƒобавление опорной точки
        void AddIntermediateControlPoint(Vector3 position)
        {
            // —оздаем опорную точку с использованием префаба дл€ промежуточных точек
            GameObject intermediateControlPoint = Instantiate(intermediatePointPrefab, position, Quaternion.identity);
            spline.controlPoints.Add(intermediateControlPoint.transform);
            isControl.Add(false);
        }
    



}
