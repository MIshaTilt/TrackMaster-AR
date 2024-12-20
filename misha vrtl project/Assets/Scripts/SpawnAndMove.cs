using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndMove : MonoBehaviour
{
    public Transform objectA;
    public Transform objectB;

    public float X;


    // Start is called before the first frame update
    void Start()
    {
        
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

        
    }
}
