using Meta.WitAi;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private GameObject check;
    public GameObject screen;

    public Image myNewBar;
    public OVRInput.RawButton changeMode;
    public bool isDriving = false;
    public bool isChanging = false;

    public float reloadTime = 1f;
    private float reloadTimer;

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
            instance = null;
            check.SetActive(false);
            check = null;
            StartCoroutine(Reset());
        }
        if (buttons[1].localScale.x != 1 && pressed == false)
        {
            pressed = true;

            if (check != null)
            {
                check.SetActive(false);
                check = null;
            }
            check = checks[1];
            check.SetActive(true);
            instance.DestroySafely();
            instance = null;
            instance = Instantiate(prefabs[1], spawn.position, Quaternion.identity);
            StartCoroutine(Reset());
        }if (buttons[2].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            if (check != null)
            {
                check.SetActive(false);
                check = null;
            }
            check = checks[2];
            check.SetActive(true);
            instance.DestroySafely();
            instance = null;
            instance = Instantiate(prefabs[2], spawn.position, Quaternion.identity);
            StartCoroutine(Reset());
        }if (buttons[3].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            if (check != null)
            {
                check.SetActive(false);
                check = null;
            }
            check = checks[3];
            check.SetActive(true);
            instance.DestroySafely();
            instance = null;
            instance = Instantiate(prefabs[3], spawn.position, Quaternion.identity);
            StartCoroutine(Reset());
        }if (buttons[4].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            if (check != null)
            {
                check.SetActive(false);
                check = null;
            }
            check = checks[4];
            check.SetActive(true);
            instance.DestroySafely();
            instance = null;
            instance = Instantiate(prefabs[4], spawn.position, Quaternion.identity);
            StartCoroutine(Reset());
        }if (buttons[5].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            if (check != null)
            {
                check.SetActive(false);
                check = null;
            }
            check = checks[5];
            check.SetActive(true);
            instance.DestroySafely();
            instance = null;
            instance = Instantiate(prefabs[5], spawn.position, Quaternion.identity);
            StartCoroutine(Reset());
        }if (buttons[6].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            if (check != null)
            {
                check.SetActive(false);
                check = null;
            }
            check = checks[6];
            check.SetActive(true);
            instance.DestroySafely();
            instance = null;
            instance = Instantiate(prefabs[6], spawn.position, Quaternion.identity);
            StartCoroutine(Reset());
        }


        if(instance != null)
        {

        }

        if (OVRInput.GetDown(changeMode))
        {
            isChanging = true;
        }
        if (OVRInput.GetUp(changeMode))
        {
            isChanging = false;
        }
        if (isChanging)
        {
            reloadTimer += Time.deltaTime;
            myNewBar.fillAmount = reloadTimer / reloadTime;
            if (reloadTimer > reloadTime)
            {
                isChanging = false;
                myNewBar.fillAmount = 0f;
                ChangeMode();
                Debug.Log("Hi");
                return;
            }
        }
        else
        {
            myNewBar.fillAmount = 0f;
            reloadTimer = 0f;
        }

    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        pressed = false;
    }

    public void ChangeMode()
    {
        if (isDriving)
        {
            screen.SetActive(false);
            isDriving = false;
            Debug.Log("1");
        }
        else if (!isDriving)
        {
            screen.SetActive(true);
            isDriving = true;
            Debug.Log("2");
        }
    }
}
