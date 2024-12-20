using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections.Generic;
using UnityEngine;

public class MirrorObjects : MonoBehaviour
{
    // Ссылки на объекты
    public Transform objectA;  // Первый объект
    public Transform objectB;  // Второй объект
    public GrabInteractable interactorA;
    public GrabInteractable interactorB;
    public GrabInteractable interactorMid;
    private bool firstFrame = true;
    private Vector3 initilalAOffset;
    private Vector3 initilalBOffset;
    public LineRenderer lineRenderer;


    private enum ControllerButtonState { None, Left, Right, Mid } // Перечисление для состояния кнопок

    private ControllerButtonState lastButtonPressed = ControllerButtonState.None; // Переменная для хранения последнего состояния


    void Update()
    {
        if(interactorA != null && interactorB != null && interactorMid != null)
        {
            // Проверяем, нажата ли левая кнопка
            if (interactorA.State == InteractableState.Select) // К примеру, для левой кнопки контроллера
            {
                lastButtonPressed = ControllerButtonState.Left;
            }

            // Проверяем, нажата ли правая кнопка
            if (interactorB.State == InteractableState.Select) // К примеру, для правой кнопки контроллера
            {
                lastButtonPressed = ControllerButtonState.Right;
            }
            if (interactorMid.State == InteractableState.Select) // К примеру, для правой кнопки контроллера
            {
                lastButtonPressed = ControllerButtonState.Mid;
            }

            // Логика, которая выполняется в зависимости от последнего нажатия
            switch (lastButtonPressed)
            {
                case ControllerButtonState.Left:
                    ExecuteLeftAction();
                    break;
                case ControllerButtonState.Right:
                    ExecuteRightAction();
                    break;
                case ControllerButtonState.Mid:
                    ExecuteMiddleAction();
                    break;
                case ControllerButtonState.None:
                    MoveOnStart();
                    break;
            }

            LineCatcher();
        }

        else
        {
            lineRenderer.enabled = false;
        }
        
    }

    private void LineCatcher()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, objectA.position); // Начальная точка
        lineRenderer.SetPosition(1, objectB.position); // Конечная точка
    }

    private void MoveOnStart()
    {
        Vector3 mirrorOffset = objectA.position - transform.position;
        objectB.position = transform.position - mirrorOffset;

        // Отражение поворота ObjectB относительно ObjectA
        Quaternion mirrorRotation = Quaternion.Inverse(objectA.rotation);
        objectB.rotation = transform.rotation * mirrorRotation;


        firstFrame = true;
    }

    // Действия для левой кнопки
    private void ExecuteLeftAction()
    {
        // Сохраняем текущее положение объекта B
        Vector3 currentBPosition = objectB.position;
        Quaternion currentBRotation = objectB.rotation;

        // Отражаем объектA относительно центра
        Vector3 mirrorOffset = objectA.position - transform.position;
        objectB.position = transform.position - mirrorOffset;

        firstFrame = true;
    }

    // Действия для правой кнопки
    private void ExecuteRightAction()
    {
        // Сохраняем текущее положение объекта B
        Vector3 currentAPosition = objectA.position;
        Quaternion currentARotation = objectA.rotation;

        // Отражаем объектA относительно центра
        Vector3 mirrorOffset = objectB.position - transform.position;
        objectA.position = transform.position - mirrorOffset;

        firstFrame = true;
    }
    private void ExecuteMiddleAction()
    {
        if (firstFrame)
        {
            initilalAOffset = objectA.transform.position - transform.position;
            initilalBOffset = objectB.transform.position - transform.position;
            firstFrame = false;
        }
        else
        {
            objectA.transform.position = transform.position + initilalAOffset;
            objectB.transform.position = transform.position + initilalBOffset;
        }
    }

}
