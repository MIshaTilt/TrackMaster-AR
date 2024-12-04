using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortsHandler : MonoBehaviour
{
    public HandGrabInteractable grabbed1;
    public HandGrabInteractable grabbed2;
    public GrabInteractable grabbed3;
    public bool active = true;

    public GameObject leftIn;
    public GameObject leftOut;
    public GameObject leftInd;
    public GameObject rightIn;
    public GameObject rightOut;
    public GameObject rightInd;
    public bool left = false;
    public bool oneWay = false;

    public TextMeshProUGUI act;
    public TextMeshProUGUI lef;

    public GameObject main;

    public bool isStatic = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        act.text = active.ToString();
        lef.text = left.ToString();

        if ((grabbed1.State == InteractableState.Select || grabbed2.State == InteractableState.Select || grabbed3.State == InteractableState.Select) && active == true && oneWay == false)
        {
            leftIn.SetActive(false);
            rightIn.SetActive(false);
            active = false;
        }
        else if (grabbed1.State == InteractableState.Normal && grabbed2.State == InteractableState.Normal && grabbed3.State == InteractableState.Normal && active == false && oneWay == false) { 
            StartCoroutine(SetAct()); }

        if(isStatic == true)
        {
            main.transform.rotation = Quaternion.Euler(0f, main.transform.rotation.eulerAngles.y, 0f);

        }
    }

    private IEnumerator SetAct()
    {
        yield return new WaitForSeconds(1);
        if (!left)
        {
            leftIn.SetActive(true);
        }
        else
        {
            rightIn.SetActive(true);
        }

        active = true;
    }

    public void ChangeDir()
    {
        if(left)
        {
            left = false;
            //leftIn.SetActive(true);
            leftOut.SetActive(false);
            leftInd.SetActive(false);
            rightIn.SetActive(false);
            rightOut.SetActive(true);
            rightInd.SetActive(true);
            if(active==true)
            {
                leftIn.SetActive(true);
            }
        }
        else
        {
            left = true;
            leftIn.SetActive(false);
            leftOut.SetActive(true);
            leftInd.SetActive(true);
            //rightIn.SetActive(true);
            rightOut.SetActive(false);
            rightInd.SetActive(false);
            if (active == true)
            {
                rightIn.SetActive(true);
            }
        }
    }

    public void ChangeDirOnePort()
    {
        if (left)
        {
            left = false;
            rightIn.SetActive(true);
            rightOut.SetActive(false);
            leftInd.SetActive(false);
            rightInd.SetActive(true);
        }
        else
        {
            left = true;
            rightIn.SetActive(false);
            rightOut.SetActive(true);
            leftInd.SetActive(true);
            rightInd.SetActive(false);
        }
    }


    public void Del()
    {
        StartCoroutine(Delete());
    }

    private IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(main);
    }
}
