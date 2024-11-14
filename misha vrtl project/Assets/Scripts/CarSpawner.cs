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

    public CustomPrometeoCarController1 carController;

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
            isDriving = true;
            instance.DestroySafely();
            instance = null;
            check.SetActive(false);
            check = null;
            screen.SetActive(false);
            StartCoroutine(Reset());
        }
        if (buttons[1].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            EnableCar(1);
            StartCoroutine(Reset());
        }if (buttons[2].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            EnableCar(2);
            StartCoroutine(Reset());
        }if (buttons[3].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            EnableCar(3);
            StartCoroutine(Reset());
        }if (buttons[4].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            EnableCar(4);
            StartCoroutine(Reset());
        }if (buttons[5].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            EnableCar(5);
            StartCoroutine(Reset());
        }if (buttons[6].localScale.x != 1 && pressed == false)
        {
            pressed = true;
            EnableCar(6);
            StartCoroutine(Reset());
        }


        if(instance != null)
        {

        }

        if (OVRInput.GetDown(changeMode) && instance != null)
        {
            isChanging = true;
        }
        if (OVRInput.GetUp(changeMode) && instance != null)
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
            ScreenDisable();
            isDriving = false;
            Debug.Log("1");
        }
        else if (!isDriving)
        {
            ScreenEnable();
            isDriving = true;
            Debug.Log("2");
        }
    }

    public void ScreenEnable()
    {
        screen.SetActive(true);
        carController.enabled = true;
    }
    public void ScreenDisable()
    {
        screen.SetActive(false);
        carController.enabled = false;
    }
    public void EnableCar(int n)
    {

        if (check != null)
        {
            check.SetActive(false);
            check = null;
        }
        check = checks[n];
        check.SetActive(true);
        screen.SetActive(true);
        instance.DestroySafely();
        instance = null;
        instance = Instantiate(prefabs[n], spawn.position, Quaternion.identity);
        carController = instance.GetComponent<CustomPrometeoCarController1>();
    }
}
