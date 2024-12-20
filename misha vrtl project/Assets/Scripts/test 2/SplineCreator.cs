using System.Collections.Generic;
using UnityEngine;

public class SplineCreator : MonoBehaviour
{

    public GameObject controlPointPrefab;  // ѕрефаб дл€ основных контрольных точек
    public GameObject intermediatePointPrefab;  // ѕрефаб дл€ опорных (промежуточных) точек

    private BezierSpline spline;

    public List<bool> isControl = new List<bool>();

    public Transform rHandAnchor;
    public OVRInput.RawButton rAdd;

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
        if (OVRInput.GetDown(rAdd))
        {
            AddNewControlPoint(rHandAnchor.position);
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
        

        // ≈сли это не перва€ точка, добавл€ем промежуточные опорные точки
        if (spline.controlPoints.Count != 0)
        {
            // –асчет позиций дл€ опорных точек
            Transform previousPoint = spline.controlPoints[spline.controlPoints.Count-1];
            Vector3 controlPoint1 = Vector3.Lerp(previousPoint.position, position, 0.33f);
            Vector3 controlPoint2 = Vector3.Lerp(previousPoint.position, position, 0.66f);

            // —оздаем и добавл€ем промежуточные опорные точки с использованием второго префаба
            AddIntermediateControlPoint(controlPoint1);
            AddIntermediateControlPoint(controlPoint2);
            Debug.Log("FE!N");
        }

        // —оздаем основную контрольную точку с использованием первого префаба
        GameObject newControlPoint = Instantiate(controlPointPrefab, position, Quaternion.identity);
        newControlPoint.transform.rotation=Quaternion.Euler(new Vector3(0,90,0));
        spline.controlPoints.Add(newControlPoint.transform);
        isControl.Add(true);
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
