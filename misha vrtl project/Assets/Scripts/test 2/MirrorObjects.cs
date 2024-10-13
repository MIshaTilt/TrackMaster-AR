using UnityEngine;

public class MirrorObjects : MonoBehaviour
{
    // ������ �� �������
    public Transform objectA;  // ������ ������
    public Transform objectB;  // ������ ������

    private void Update()
    {
        if (objectA != null && objectB != null)
        {
            // ��������� ������� ��������� ������� B
            Vector3 currentBPosition = objectB.position;
            Quaternion currentBRotation = objectB.rotation;

            // �������� ������A ������������ ������
            Vector3 mirrorOffset = objectA.position - transform.position;
            objectB.position = transform.position - mirrorOffset;

            // ���������, �������� �� ������ B
            if (currentBPosition != objectB.position)
            {
                // ���� ������ B ������������, �������� �������� ������� A ������������ B
                Vector3 offsetFromB = objectB.position - currentBPosition;
                objectA.position += offsetFromB;

                // ����� ����� ��������� �������� �������, ���� ��� ����������
                Quaternion mirrorRotation = Quaternion.Inverse(currentBRotation);
                objectA.rotation = transform.rotation * mirrorRotation;
            }
        }
    }
}
