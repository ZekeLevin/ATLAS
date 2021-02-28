using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffIntLights : MonoBehaviour
{
    public static bool lightsOn = true;
    public GameObject[] lights;
    public GameObject clawLight;
    public GameObject[] screens;
    public GameObject offSolarLight;
    private bool areLightsOn;
    private bool isClawOn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLights());
        //GagueScript.energyDrainRate += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(lightsOn)
            GagueScript.energyLevel -= Time.deltaTime*GagueScript.drainModifier;
        if (Input.GetKeyDown(KeyCode.T) && GagueScript.energyLevel > 0)
        {
            if (lightsOn == true)
            {
                TurnOffLights();
                return;
            }
            if (lightsOn == false)
            {
                TurnOnLights();
            }
        }

        if (GagueScript.energyLevel <= 0)
        {
            if (lightsOn)
            {
                areLightsOn = true;
            }
            else
            {
                areLightsOn = false;
            }
            if (iKScript.moduleOn)
            {
                isClawOn = true;
            }
            else
            {
                isClawOn = false;
            }

            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
            clawLight.SetActive(false);
            foreach(GameObject screen in screens)
            {
                screen.SetActive(false);
            }
            iKScript.moduleOn = false;

        }

        if(NarrativeScript.recharged)
        {
            if (areLightsOn)
            {
                foreach (GameObject light in lights)
                {
                    light.SetActive(true);
                }
                clawLight.SetActive(true);
                foreach (GameObject screen in screens)
                {
                    screen.SetActive(true);
                }
            }
            if (isClawOn)
            {
                iKScript.moduleOn = true;
            }
            NarrativeScript.recharged = false;
        }

    }

    void TurnOnLights()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }
        offSolarLight.SetActive(false);
        lightsOn = true;
    }

    void TurnOffLights()
    {
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
        offSolarLight.SetActive(true);
        lightsOn = false;
    }

    IEnumerator StartLights()
    {
        TurnOffLights();
        yield return new WaitForSeconds(35);
        TurnOnLights();
    }
}
