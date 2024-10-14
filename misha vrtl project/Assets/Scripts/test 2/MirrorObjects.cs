using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class MirrorObjects : MonoBehaviour
{
    // ������ �� �������
    public Transform objectA;  // ������ ������
    public Transform objectB;  // ������ ������
    public GrabInteractable interactorA;
    public GrabInteractable interactorB;

    /*private void Update()
    {
        if (objectA != null && objectB != null)
        {
            // ��������� ������� ��������� ������� B
            Vector3 currentBPosition = objectB.position;
            Quaternion currentBRotation = objectB.rotation;

            // �������� ������A ������������ ������
            Vector3 mirrorOffset = objectA.position - transform.position;
            objectB.position = transform.position - mirrorOffset;
        }

        if (interactorA.State == InteractorState.Select)
        {

        }
    }*/

    private enum ControllerButtonState { None, Left, Right } // ������������ ��� ��������� ������

    private ControllerButtonState lastButtonPressed = ControllerButtonState.None; // ���������� ��� �������� ���������� ���������

    void Update()
    {
        // ���������, ������ �� ����� ������
        if (interactorA.State == InteractableState.Select) // � �������, ��� ����� ������ �����������
        {
            lastButtonPressed = ControllerButtonState.Left;
            Debug.Log("��������� ������� ������: A");
        }

        // ���������, ������ �� ������ ������
        if (interactorB.State == InteractableState.Select) // � �������, ��� ������ ������ �����������
        {
            lastButtonPressed = ControllerButtonState.Right;
            Debug.Log("��������� ������� ������: B");
        }

        // ������, ������� ����������� � ����������� �� ���������� �������
        switch (lastButtonPressed)
        {
            case ControllerButtonState.Left:
                ExecuteLeftAction();
                break;
            case ControllerButtonState.Right:
                ExecuteRightAction();
                break;
        }
    }

    // �������� ��� ����� ������
    private void ExecuteLeftAction()
    {
        // ���� ������ ��� �������� ��� ������� ����� ������
        Debug.Log("����������� �������� ��� ����� ������");
        // ��������� ������� ��������� ������� B
        Vector3 currentBPosition = objectB.position;
        Quaternion currentBRotation = objectB.rotation;

        // �������� ������A ������������ ������
        Vector3 mirrorOffset = objectA.position - transform.position;
        objectB.position = transform.position - mirrorOffset;
    }

    // �������� ��� ������ ������
    private void ExecuteRightAction()
    {
        // ���� ������ ��� �������� ��� ������� ������ ������
        Debug.Log("����������� �������� ��� ������ ������");
        // ��������� ������� ��������� ������� B
        Vector3 currentAPosition = objectA.position;
        Quaternion currentARotation = objectA.rotation;

        // �������� ������A ������������ ������
        Vector3 mirrorOffset = objectB.position - transform.position;
        objectA.position = transform.position - mirrorOffset;
    }

}
