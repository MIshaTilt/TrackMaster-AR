using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public List<RectTransform> buttons;
    public List<GameObject> prefabs;
    public List<GameObject> checks;
    public Transform spawn;
    private bool pressed;

    public Rigidbody newIsSnappedValue;


    private GameObject instance;
    private GameObject activebutton;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (buttons[0].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            instance.DestroySafely();
            checks[1].SetActive(false);
            StartCoroutine(Reset());
        }
        if (buttons[1].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            checks[1].SetActive(true);
            instance = Instantiate(prefabs[1], spawn.position, Quaternion.identity);
            StartCoroutine(Reset());
        }


    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        pressed = false;
    }
}
