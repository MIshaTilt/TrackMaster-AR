using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public List<RectTransform> buttons;
    public List<GameObject> prefabs;
    public Transform spawn;
    private bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttons[0].localScale.x !=1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[0], spawn.position,Quaternion.identity);
            StartCoroutine(Reset());
        }
        if (buttons[1].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[1], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[2].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[2], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[3].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[3], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[4].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[4], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[5].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[5], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[6].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[6], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[7].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[7], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[8].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[8], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[9].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[9], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[10].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[10], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[11].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[11], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[12].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[12], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[13].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[13], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[14].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[14], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[15].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[15], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[16].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[16], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[17].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[17], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[18].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[18], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[19].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[19], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[20].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[20], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[21].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[21], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[22].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[22], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[23].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[23], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[24].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[24], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
        if (buttons[25].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            Instantiate(prefabs[25], spawn.position, Quaternion.identity); StartCoroutine(Reset());

        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        pressed = false;
    }


}
