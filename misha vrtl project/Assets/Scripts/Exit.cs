using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public List<RectTransform> buttons;

    private bool pressed;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (buttons[0].localScale.x != 1 && pressed == false)
        {
            OVRScene.RequestSpaceSetup();
        }
        if (buttons[1].localScale.x != 1 && pressed == false)
        {
            Application.Quit();
        }
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        pressed = false;
    }
}
