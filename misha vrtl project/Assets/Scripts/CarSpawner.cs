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
    public GameObject screenInner;

    public Image myNewBar;
    public OVRInput.RawButton changeMode;
    public bool isDriving = false;
    public bool isChanging = false;

    public float reloadTime = 1f;
    private float reloadTimer;

    public GameObject rightController;
    public GameObject rightControllerInteraction;
    //public GameObject notification;

    public Settings settings;

    public GameObject handControl;
    public Transform rightHand;

    public MusicPlayer musicPlayer;

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
            screen.SetActive(false);
            rightControllerInteraction.SetActive(true);
            handControl.GetComponent<HandControl>().controller = null;
            StartCoroutine(Reset());
            musicPlayer.audioSource = null;
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

        if (rightController.active == false && instance != null && carController.enabled==true && handControl.active == false)
        {
            ScreenDisable();
            HandEnable();
            Debug.Log("1");
        }
        else if(rightController.active == true && instance != null && handControl.active == true)
        {
            HandDisable();
            ScreenEnable();
            //isDriving = true;
            Debug.Log("2");

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
        rightControllerInteraction.SetActive(false);
        //carController.enabled = true;
        Debug.Log("screen tryna active");
        
    }
    public void ScreenDisable()
    {
        screen.SetActive(false);
        rightControllerInteraction.SetActive(true);
        //carController.enabled = false;
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
        if (rightController.active == true)
        {
            screen.SetActive(true);


        }
        rightControllerInteraction.SetActive(false);
        isDriving = true;
        musicPlayer.audioSource = null;
        instance.DestroySafely();
        instance = null;
        instance = Instantiate(prefabs[n], spawn.position, Quaternion.identity);
        carController = instance.GetComponent<CustomPrometeoCarController1>();
        handControl.GetComponent<HandControl>().controller = carController;
        var lights = instance.GetComponentsInChildren<Light>();
        foreach (Light light in lights)
        {
            light.enabled = settings.needLights;
        }
        //musicPlayer.audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    public void HandEnable()
    {
        handControl.SetActive(true);
        handControl.transform.position = rightHand.transform.position + new Vector3(0, -1, 0);
    }

    public void HandDisable()
    {
        handControl.SetActive(false);
    }

    public void enaFpv(bool index)
    {
        if(index == true)
        {
            screenInner.SetActive(true);
        }
        else
        {
            screenInner.SetActive(false);
        }
    }
}
