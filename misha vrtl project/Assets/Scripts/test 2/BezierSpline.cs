using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using static SplineDone;
using UnityEngine.Experimental.GlobalIllumination;

public class BezierSpline : MonoBehaviour
{
    public List<Transform> controlPoints = new List<Transform>(); // ������ ���� ����������� � ������� �����

    public LineRenderer lineRenderer;  // LineRenderer ��� ��������� �����

    public int resolution = 20;

    public event EventHandler OnDirty;

    private float splineLength;

    private List<Vector3> pointList;

    private void Start()
    {
        lineRenderer.positionCount = 0;  // ���������� �������������, ��� ��� ����� ��� ����������
    }

    private void Update()
    {
        DrawBezierCurve();
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
        pointList = curvePoints;
        // ������������� ���������� ����� ��� LineRenderer
        lineRenderer.positionCount = curvePoints.Count;
        lineRenderer.SetPositions(curvePoints.ToArray());
    }

    public List<Transform> GetInterpolatedTransforms()
    {
        if (controlPoints.Count < 4)
        {
            //Debug.LogError("������������ ����� ��� �������� �������. ����� ������� 4 �����.");
            return new List<Transform>();
        }

        List<Transform> interpolatedTransforms = new List<Transform>();

        // �������� �� ������ ������ �� 4 ����� (��� ���������� ������ �����)
        for (int i = 0; i <= controlPoints.Count - 4; i += 3)
        {
            for (int j = 0; j < resolution; j++)
            {
                float t = (float)j / (resolution - 1); // ����������� t �� 0 �� 1
                Vector3 point = CalculateBezierPoint(t, controlPoints[i].position, controlPoints[i + 1].position, controlPoints[i + 2].position, controlPoints[i + 3].position);

                Quaternion rotation = CalculateBezierRotation(t, controlPoints[i].rotation, controlPoints[i + 3].rotation);


                // ������� ����� ������ ������ � Transform ��� ������ ����������������� �����
                GameObject newPoint = new GameObject("InterpolatedPoint");
                newPoint.transform.position = point;
                newPoint.transform.rotation = rotation;

                // ��������� Transform ����� ������� � ������
                interpolatedTransforms.Add(newPoint.transform);
                Destroy(newPoint);
            }
        }

        return interpolatedTransforms;
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

    private Quaternion CalculateBezierRotation(float t, Quaternion startRotation, Quaternion endRotation)
    {
        return Quaternion.Slerp(startRotation, endRotation, t); // ������������ ���������
    }


    public void SetDirty()
    {
        splineLength = GetSplineLength();

        OnDirty?.Invoke(this, EventArgs.Empty);
    }

    public float GetSplineLength(float stepSize = .01f)
    {
        float splineLength = 0f;

        Vector3 lastPosition = GetPositionAt(0f);

        for (float t = 0; t < 1f; t += stepSize)
        {
            splineLength += Vector3.Distance(lastPosition, GetPositionAt(t));

            lastPosition = GetPositionAt(t);
        }

        splineLength += Vector3.Distance(lastPosition, GetPositionAt(1f));

        return splineLength;
    }

    public Vector3 GetPositionAt(float t)
    {
        if (t == 1)
        {
            // Full position, special case

            var anchorA = controlPoints[controlPoints.Count - 3];
            var handle1 = controlPoints[controlPoints.Count - 2];
            var handle2 = controlPoints[controlPoints.Count - 1];
            var anchorB = controlPoints[controlPoints.Count];

            return transform.position + CubicLerp(anchorA.position, handle1.position, handle2.position, anchorB.position, t);
        }
        else
        {
            float tFull = t * (controlPoints.Count/3);
            int anchorIndex = Mathf.FloorToInt(tFull) * 3;
            float tAnchor = tFull - anchorIndex;

            Transform anchorA, anchorB, handle1, handle2;

            if (anchorIndex < controlPoints.Count - 1)
            {
                anchorA = controlPoints[anchorIndex + 0];
                handle1 = controlPoints[anchorIndex + 1];
                handle2 = controlPoints[anchorIndex + 2];
                anchorB = controlPoints[anchorIndex + 3];
            }
            else
            {
                // anchorIndex is final one, either don't link to "next" one or loop back

                anchorA = controlPoints[anchorIndex - 3];
                handle1 = controlPoints[anchorIndex - 2];
                handle2 = controlPoints[anchorIndex - 1];
                anchorB = controlPoints[anchorIndex + 0];
                tAnchor = 1f;

            }

            return transform.position + CubicLerp(anchorA.position, handle1.position, handle2.position, anchorB.position, tAnchor);
        }
    }

    private Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab, bc, t);
    }

    private Vector3 CubicLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 abc = QuadraticLerp(a, b, c, t);
        Vector3 bcd = QuadraticLerp(b, c, d, t);

        return Vector3.Lerp(abc, bcd, t);
    }

    public List<Transform> GetPointList()
    {
        return controlPoints;
    }


}
