using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEngine;

public class MirrorObjects : MonoBehaviour
{
    // Ссылки на объекты
    public Transform objectA;  // Первый объект
    public Transform objectB;  // Второй объект
    public GrabInteractable interactorA;
    public GrabInteractable interactorB;

    /*private void Update()
    {
        if (objectA != null && objectB != null)
        {
            // Сохраняем текущее положение объекта B
            Vector3 currentBPosition = objectB.position;
            Quaternion currentBRotation = objectB.rotation;

            // Отражаем объектA относительно центра
            Vector3 mirrorOffset = objectA.position - transform.position;
            objectB.position = transform.position - mirrorOffset;
        }

        if (interactorA.State == InteractorState.Select)
        {

        }
    }*/

    private enum ControllerButtonState { None, Left, Right } // Перечисление для состояния кнопок

    private ControllerButtonState lastButtonPressed = ControllerButtonState.None; // Переменная для хранения последнего состояния

    void Update()
    {
        // Проверяем, нажата ли левая кнопка
        if (interactorA.State == InteractableState.Select) // К примеру, для левой кнопки контроллера
        {
            lastButtonPressed = ControllerButtonState.Left;
            Debug.Log("Последняя нажатая кнопка: A");
        }

        // Проверяем, нажата ли правая кнопка
        if (interactorB.State == InteractableState.Select) // К примеру, для правой кнопки контроллера
        {
            lastButtonPressed = ControllerButtonState.Right;
            Debug.Log("Последняя нажатая кнопка: B");
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
        }
    }

    // Действия для левой кнопки
    private void ExecuteLeftAction()
    {
        // Ваша логика для действия при нажатии левой кнопки
        Debug.Log("Выполняется действие для левой кнопки");
        // Сохраняем текущее положение объекта B
        Vector3 currentBPosition = objectB.position;
        Quaternion currentBRotation = objectB.rotation;

        // Отражаем объектA относительно центра
        Vector3 mirrorOffset = objectA.position - transform.position;
        objectB.position = transform.position - mirrorOffset;
    }

    // Действия для правой кнопки
    private void ExecuteRightAction()
    {
        // Ваша логика для действия при нажатии правой кнопки
        Debug.Log("Выполняется действие для правой кнопки");
        // Сохраняем текущее положение объекта B
        Vector3 currentAPosition = objectA.position;
        Quaternion currentARotation = objectA.rotation;

        // Отражаем объектA относительно центра
        Vector3 mirrorOffset = objectB.position - transform.position;
        objectA.position = transform.position - mirrorOffset;
    }

}
