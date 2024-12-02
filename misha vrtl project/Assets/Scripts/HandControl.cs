using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandControl : MonoBehaviour
{
    [Space(10)]
    public LineRenderer fwdLineRenderer;
    public LineRenderer bckwLineRenderer;
    public LineRenderer leftLineRenderer;
    public LineRenderer rightLineRenderer;

    /*[Space(10)]
    public Transform fwdLineTransform;
    public Transform bckwLineTransform;
    public Transform leftLineTransform;
    public Transform rightLineTransform;*/

    [Space(10)]
    public Transform fwdGuideTransform;
    public Transform bckwGuideTransform;
    public Transform leftGuideTransform;
    public Transform rightGuideTransform;

    [Space(10)]
    public Transform fwdHandTransform;
    public Transform bckwHandTransform;
    public Transform leftHandTransform;
    public Transform rightHandTransform;

    [Space(10)]
    public TextMeshProUGUI fwdText;
    public TextMeshProUGUI bckwText;
    public TextMeshProUGUI leftText;
    public TextMeshProUGUI rightText;

    public bool fwd;
    public bool bckw;
    public bool left;
    public bool right;

    public Collider underPanel;

    public float maxDist = 0.6216564f;
    public float fwdFactor;
    public float bckwFactor;
    public float leftFactor;
    public float rightFactor;

    public CustomPrometeoCarController1 controller;

    // Start is called before the first frame update
    void Start()
    {
        fwdLineRenderer.positionCount = 2;
        bckwLineRenderer.positionCount = 2;
        leftLineRenderer.positionCount = 2;
        rightLineRenderer.positionCount = 2;

    }


    // Update is called once per frame
    void Update()
    {
        if (fwd)
        {
            FwdActive();
        }
        if (bckw)
        {
            BckwActive();
        }
        if (left)
        {
            LeftActive();
        }
        if (right)
        {
            RightActive();
        }
    }

    public void FwdActive()
    {
        fwdGuideTransform.position = Physics.ClosestPoint(fwdHandTransform.position, underPanel, gameObject.transform.position,gameObject.transform.rotation);
        fwdLineRenderer.SetPosition(0, fwdGuideTransform.position);
        fwdLineRenderer.SetPosition(1, fwdHandTransform.position);
        float dist = Vector3.Distance(fwdGuideTransform.position, fwdHandTransform.position);
        fwdFactor = ((maxDist - dist) / maxDist);
        if (fwdFactor < 0)
        {
            fwdFactor = 0;
        }
        fwdText.SetText($"FWD: {RoundToDecimalPlaces(fwdFactor * 100, 0)}%");
        controller.GoForward(fwdFactor);
    }

    public void BckwActive()
    {
        bckwGuideTransform.position = Physics.ClosestPoint(bckwHandTransform.position, underPanel, gameObject.transform.position, gameObject.transform.rotation);

        bckwLineRenderer.SetPosition(0, bckwGuideTransform.position);
        bckwLineRenderer.SetPosition(1, bckwHandTransform.position);
        float dist = Vector3.Distance(bckwGuideTransform.position, bckwHandTransform.position);
        bckwFactor = ((maxDist - dist) / maxDist);
        if (bckwFactor < 0) { bckwFactor = 0;}
        bckwText.SetText($"BCKW: {RoundToDecimalPlaces(bckwFactor * 100, 0)}%");
        controller.GoReverse(bckwFactor);
    }

    public void LeftActive()
    {
        leftGuideTransform.position = Physics.ClosestPoint(leftHandTransform.position, underPanel, gameObject.transform.position, gameObject.transform.rotation);

        leftLineRenderer.SetPosition(0, leftGuideTransform.position);
        leftLineRenderer.SetPosition(1, leftHandTransform.position);
        float dist = Vector3.Distance(leftGuideTransform.position, leftHandTransform.position);
        leftFactor =((maxDist - dist) / maxDist);
        if (leftFactor < 0) {  leftFactor = 0;}
        leftText.SetText($"LEFT: {RoundToDecimalPlaces(leftFactor * 100, 0)}%");
        if(leftFactor > rightFactor)
        {
            controller.TurnLeft(-leftFactor);

        }
        //controller.TurnLeft(-1);
    }

    public void RightActive()
    {
        rightGuideTransform.position = Physics.ClosestPoint(rightHandTransform.position, underPanel, gameObject.transform.position, gameObject.transform.rotation);

        rightLineRenderer.SetPosition(0, rightGuideTransform.position);
        rightLineRenderer.SetPosition(1, rightHandTransform.position);
        float dist = Vector3.Distance(rightGuideTransform.position, rightHandTransform.position);
        rightFactor = ((maxDist - dist) / maxDist);
        if (rightFactor < 0) {  rightFactor = 0;}
        rightText.SetText($"RIGHT: {RoundToDecimalPlaces(rightFactor * 100, 0)}%");
        if (rightFactor > leftFactor)
        {
            controller.TurnRight(rightFactor);

        }
        //controller.TurnRight(1);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "fwd") { 
            fwd = true; 
            fwdLineRenderer.enabled = true; 
        }
        else if(other.gameObject.tag == "bckw") { 
            bckw = true; 
            bckwLineRenderer.enabled = true; }
        else if(other.gameObject.tag == "left") { 
            left = true; 
            leftLineRenderer.enabled = true; }
        else if (other.gameObject.tag == "rifgt") { 
            right = true; 
            rightLineRenderer.enabled = true; }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "fwd") { 
            fwd = false; 
            fwdLineRenderer.enabled = false;
            fwdFactor = 0;
            fwdText.SetText($"FWD: 0%");
            controller.GoForward(0);
        }
        else if (other.gameObject.tag == "bckw") { 
            bckw = false; 
            bckwLineRenderer.enabled = false; 
            bckwFactor = 0;
            bckwText.SetText($"BCKW: 0%");
            controller.GoReverse(0);

        }
        else if (other.gameObject.tag == "left") { 
            left = false; 
            leftLineRenderer.enabled = false;
            leftFactor = 0;
            leftText.SetText($"LEFT: 0%");
            controller.TurnLeft(0);
        }
        else if (other.gameObject.tag == "rifgt") { 
            right = false; 
            rightLineRenderer.enabled = false; 
            rightFactor = 0;
            rightText.SetText($"RIGHT: 0%");
            controller.TurnRight(0);
        }
    }

    public float RoundToDecimalPlaces(float value, int decimalPlaces)
    {
        float scale = Mathf.Pow(10f, decimalPlaces);
        return Mathf.Round(value * scale) / scale;
    }
}
