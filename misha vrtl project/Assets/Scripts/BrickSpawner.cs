using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public OVRInput.RawButton add;
    public GameObject brick;
    public Transform hand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(add))
        {
            Instantiate(brick,hand);
        }
    }
}
