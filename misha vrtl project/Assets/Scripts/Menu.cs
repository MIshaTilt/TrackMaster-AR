using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public List<RectTransform> buttons;
    public List<GameObject> dynamicPrefabs;
    public List<GameObject> staticPrefabs;
    public List<GameObject> noPropsPrefabs;
    public Transform spawn;
    private bool pressed;

    public Rigidbody newIsSnappedValue;
    public Settings settings;


    public List<Sprite> propsSprites;
    public List<Sprite> noPropsSprites;
    public List<Image> icons;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (buttons[0].localScale.x !=1 && pressed == false)
        {
            SpawnRoad(0);
        }
        if (buttons[1].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(1);
        }
        if (buttons[2].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(2);
        }
        if (buttons[3].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(3);
        }
        if (buttons[4].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(4);
        }
        if (buttons[5].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(5);
        }
        if (buttons[6].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(6);
        }
        if (buttons[7].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(7);
        }
        if (buttons[8].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(8);
        }
        if (buttons[9].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(9);
        }
        if (buttons[10].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(10);
        }
        if (buttons[11].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(11);
        }
        if (buttons[12].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(12);
        }
        if (buttons[13].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(13);
        }
        if (buttons[14].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(14);
        }
        if (buttons[15].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(15);
        }
        if (buttons[16].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(16);
        }
        if (buttons[17].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(17);
        }
        if (buttons[18].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(18);
        }
        if (buttons[19].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(19);
        }
        if (buttons[20].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(20);
        }
        if (buttons[21].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(21);
        }
        if (buttons[22].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(22);
        }
        if (buttons[23].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(23);
        }
        if (buttons[24].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(24);
        }
        if (buttons[25].localScale.x != 1 && pressed == false)
        {
            SpawnRoad(25);
        }
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        pressed = false;
    }

    public void SpawnRoad(int n)
    {
        pressed = true;
        GameObject prefab;
        if(settings.propsInd == 0)
        {
            prefab = dynamicPrefabs[n];
        }
        else if(settings.propsInd == 1)
        {
            prefab = staticPrefabs[n];
        }
        else
        {
            prefab = noPropsPrefabs[n];
        }
        GameObject instance = Instantiate(prefab, spawn.position, Quaternion.identity);
        instance.SetActive(true);
        instance.tag = "roadsActive";
        var lights = instance.GetComponentsInChildren<Light>();
        foreach ( Light light in lights )
        {
            light.enabled = settings.needLights;
        }
        StartCoroutine(Reset());
    }

    public void ChangeSprites(bool props)
    {
        for(int i = 0; i < icons.Count; i++)
        {
            if(props == true)
            {
                icons[i].sprite = propsSprites[i];
            }
            else
            {
                icons[i].sprite = noPropsSprites[i];
            }
        }
    }
}
