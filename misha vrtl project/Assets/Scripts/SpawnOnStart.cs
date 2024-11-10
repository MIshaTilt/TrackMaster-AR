using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnStart : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;
    public Transform dialog;
    public float X;
    public OVRInput.RawButton start;
    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ���������� ������� ������� B �� ���������� X ������� ������� A
        Vector3 positionB = objectA.transform.position + (objectA.transform.forward * X);
        objectB.transform.position = positionB;

        // �������� ������� ���� �������� ������� A �� Y-���
        float rotationY = objectA.transform.eulerAngles.y;

        // ���������� ���� �������� ������� B �� Y-���, ������� ��������� ��� ��� ���������
        objectB.transform.rotation = Quaternion.Euler(0, rotationY, 0);

        if (OVRInput.GetDown(start))
        {
            menu.SetActive(true);
            menu.transform.position = dialog.position;
            Destroy(gameObject);
        }
    }
}
