using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDebug : MonoBehaviour
{
    public OVRInput.RawAxis2D VRsteering;
    public OVRInput.RawAxis1D VRtorque;
    public OVRInput.RawAxis1D VRreverse;
    public OVRInput.RawButton VRhandbrake;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(OVRInput.Get(VRreverse));
    }
}
