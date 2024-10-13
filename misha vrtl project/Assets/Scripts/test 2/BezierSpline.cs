using System.Collections.Generic;
using UnityEngine;

public class BezierSpline : MonoBehaviour
{
    public List<Transform> controlPoints = new List<Transform>(); // ������ ���� ����������� � ������� �����

    private void OnDrawGizmos()
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
    }

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
