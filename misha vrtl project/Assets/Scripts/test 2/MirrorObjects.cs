using UnityEngine;

public class MirrorObjects : MonoBehaviour
{
    // Ссылки на объекты
    public Transform objectA;  // Первый объект
    public Transform objectB;  // Второй объект

    private void Update()
    {
        if (objectA != null && objectB != null)
        {
            // Сохраняем текущее положение объекта B
            Vector3 currentBPosition = objectB.position;
            Quaternion currentBRotation = objectB.rotation;

            // Отражаем объектA относительно центра
            Vector3 mirrorOffset = objectA.position - transform.position;
            objectB.position = transform.position - mirrorOffset;

            // Проверяем, движется ли объект B
            if (currentBPosition != objectB.position)
            {
                // Если объект B переместился, отражаем движение объекта A относительно B
                Vector3 offsetFromB = objectB.position - currentBPosition;
                objectA.position += offsetFromB;

                // Можно также зеркально отражать поворот, если это необходимо
                Quaternion mirrorRotation = Quaternion.Inverse(currentBRotation);
                objectA.rotation = transform.rotation * mirrorRotation;
            }
        }
    }
}
