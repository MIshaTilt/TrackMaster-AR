using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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
            
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        if (buttons[2].localScale.x != 1 && pressed == false)
        {
            DeleteObjects();
        }
    }
    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        pressed = false;
    }

    public void DeleteObjects()
    {
        var forDelete = GameObject.FindGameObjectsWithTag("roadsActive").Concat(GameObject.FindGameObjectsWithTag("props"));
        foreach (var obj in forDelete)
        {
            Destroy(obj);
        }
    }
}
