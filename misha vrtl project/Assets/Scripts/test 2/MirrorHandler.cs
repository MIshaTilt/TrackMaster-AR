using Dreamteck.Splines.Primitives;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorHandler : MonoBehaviour
{
    private BezierSpline spline;
    private HandGrabInteractor handGrab;

    // Start is called before the first frame update
    void Start()
    {
        spline = GetComponent<BezierSpline>();
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 1; i < spline.controlPoints.Count-5; i += 3)
        {
            var ball = spline.controlPoints[i].gameObject.GetComponent<MirrorObjects>();
            if (ball != null)
            {
                ball.objectA = spline.controlPoints[i+2];
                ball.objectB = spline.controlPoints[i+4];
            }
        }
        if (InteractorState.Select == handGrab.State)
        {

        }
    }
}
