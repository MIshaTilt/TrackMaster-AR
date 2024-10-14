using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripTest : MonoBehaviour
{
    public HandGrabInteractor interactor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Проверяем, нажата ли левая кнопка
        if (interactor.IsGrabbing) // К примеру, для левой кнопки контроллера
        {
            Debug.Log("A");
        }
    }
}
