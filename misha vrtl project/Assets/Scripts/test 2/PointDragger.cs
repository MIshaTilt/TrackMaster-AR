using UnityEngine;

public class PointDragger : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("ControlPoint"))
                {
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float distance;
            if (groundPlane.Raycast(ray, out distance))
            {
                Vector3 newPos = ray.GetPoint(distance);
                transform.position = newPos;
            }
        }
    }
}
