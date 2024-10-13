using System.Collections.Generic;
using UnityEngine;

public class SplineCreator : MonoBehaviour
{

        public GameObject controlPointPrefab;  // ������ ��� �������� ����������� �����
        public GameObject intermediatePointPrefab;  // ������ ��� ������� (�������������) �����

        private BezierSpline spline;

        public List<bool> isControl = new List<bool>();

        void Start()
        {
            spline = GetComponent<BezierSpline>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // ����� ������ ���� ��� ���������� �������� ����������� �����
            {
                Vector3 newPosition = GetMouseWorldPosition();
                AddNewControlPoint(newPosition);
            }
        }

        // ��������� ������� ���� � ������� �����������
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

        // ���������� ����� ����������� ����� � �������������� ���������� ������� �����
        void AddNewControlPoint(Vector3 position)
        {
            // ������� �������� ����������� ����� � �������������� ������� �������
            GameObject newControlPoint = Instantiate(controlPointPrefab, position, Quaternion.identity);
            spline.controlPoints.Add(newControlPoint.transform);
            isControl.Add(true);

            // ���� ��� �� ������ �����, ��������� ������������� ������� �����
            if (spline.controlPoints.Count == 2)
            {
                // ������ ������� ��� ������� �����
                Transform previousPoint = spline.controlPoints[spline.controlPoints.Count - 2];
                Vector3 controlPoint1 = Vector3.Lerp(previousPoint.position, position, 0.33f);
                Vector3 controlPoint2 = Vector3.Lerp(previousPoint.position, position, 0.66f);

                // ������� � ��������� ������������� ������� ����� � �������������� ������� �������
                AddIntermediateControlPoint(controlPoint1);
                AddIntermediateControlPoint(controlPoint2);
            //Debug.Log("1");
            }
            else if(spline.controlPoints.Count > 2)
            {
                // ������ ������� ��� ������� �����
                Transform previousPoint = spline.controlPoints[spline.controlPoints.Count - 4];
                Vector3 controlPoint1 = Vector3.Lerp(previousPoint.position, position, 0.33f);
                Vector3 controlPoint2 = Vector3.Lerp(previousPoint.position, position, 0.66f);

                // ������� � ��������� ������������� ������� ����� � �������������� ������� �������
                AddIntermediateControlPoint(controlPoint1);
                AddIntermediateControlPoint(controlPoint2);
            //Debug.Log("2");
        }
    }

        // ���������� ������� �����
        void AddIntermediateControlPoint(Vector3 position)
        {
            // ������� ������� ����� � �������������� ������� ��� ������������� �����
            GameObject intermediateControlPoint = Instantiate(intermediatePointPrefab, position, Quaternion.identity);
            spline.controlPoints.Add(intermediateControlPoint.transform);
            isControl.Add(false);
        }
    



}
