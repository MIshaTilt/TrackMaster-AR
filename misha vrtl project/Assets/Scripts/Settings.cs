using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Settings : MonoBehaviour
{
    public RenderPipelineAsset[] qualityLevels;
    public TMP_Dropdown dropdown1;
    public TMP_Dropdown dropdown2;
    public OVRPassthroughLayer passthrough;
    public bool needLights;

    public int propsInd;

    public Menu menu;

    // Start is called before the first frame update
    void OnEnable()
    {
        dropdown1.value = QualitySettings.GetQualityLevel();
        dropdown2.value = 1;
        ChangeLight(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel(int value)
    {
        QualitySettings.SetQualityLevel(value);
        QualitySettings.renderPipeline = qualityLevels[value];
    }

    public void ChangeLight(int value)
    {
        if (value == 0)
        {
            var lights = GameObject.FindGameObjectsWithTag("lights");
            foreach (var light in lights)
            {
                light.GetComponent<Light>().enabled = true;
            }
            var sun = GameObject.FindGameObjectWithTag("sun");
            sun.GetComponent<Light>().enabled = false;
            passthrough.textureOpacity = 1f;
            needLights = true;
            ChangeLevel(5);
            dropdown1.value = QualitySettings.GetQualityLevel();
        }
        if (value == 1)
        {
            var lights = GameObject.FindGameObjectsWithTag("lights");
            foreach (var light in lights)
            {
                light.GetComponent<Light>().enabled = false;
            }
            var sun = GameObject.FindGameObjectWithTag("sun");
            sun.GetComponent<Light>().enabled = true;
            passthrough.textureOpacity = 1f;
            needLights = false;
            ChangeLevel(2);
            dropdown1.value = QualitySettings.GetQualityLevel();

        }
        if (value == 2)
        {
            var lights = GameObject.FindGameObjectsWithTag("lights");
            foreach (var light in lights)
            {
                light.GetComponent<Light>().enabled = true;
            }
            var sun = GameObject.FindGameObjectWithTag("sun");
            sun.GetComponent<Light>().enabled = false;
            passthrough.textureOpacity = 0.2f;
            needLights = true;
            ChangeLevel(5);
            dropdown1.value = QualitySettings.GetQualityLevel();

        }
    }

    public void ChangeProps(int value)
    {
        propsInd = value;
        if (value == 2)
        {
            menu.ChangeSprites(false);
        }
        else
        {
            menu.ChangeSprites(true);
        }
    }
}
