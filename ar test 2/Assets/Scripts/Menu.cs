using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public OVRInput.RawButton resetButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(resetButton))
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        OVRScene.RequestSpaceSetup();
        Debug.Log("hi");
    }
}
