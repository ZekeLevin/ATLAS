using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Flashlight : MonoBehaviour
{
    public SteamVR_Action_Boolean toggleLight;
    public SteamVR_Input_Sources hand;
    public Light flashlight;
    public float maxBattery = 240;
    public Material meter;
    public Material bulb;
    private bool lightOn = false;
    private float batteryPower;


    // Start is called before the first frame update
    void Start()
    {
        batteryPower = maxBattery;
        toggleLight.AddOnStateDownListener(TriggerDown, hand);
    }


    void Update()
    {
        if (lightOn)
        {
            if (batteryPower <= 0)
            {
                lightOn = false;
                return;
            }

            //bulb.SetFloat("_EmissiveIntensity", 3.5f);
            bulb.SetColor("_EmissiveColor", new Color(1, .45f, 0, 1));
            //meter.SetFloat("_EmissiveIntensity", 1);
            batteryPower -= Time.deltaTime;

            if (batteryPower > .5f * maxBattery)
            {
                meter.SetColor("_EmissiveColor", new Color(0, 1f, 0, 1));//sets to green
            }
            if (batteryPower < .5f * maxBattery)
            {
                if (batteryPower > .25f * maxBattery)
                {
                    meter.SetColor("_EmissiveColor", new Color(1, .92f, .0016f, 1));//sets to yellow
                }
                else
                {
                    meter.SetColor("_EmissiveColor", new Color(1, 0, 0, 1));//sets to Red
                }
            }



        }
        else
        {
            //meter.SetFloat("_EmissiveIntensity", 0);
            meter.SetColor("_EmissiveColor", new Color(0, 0, 0, 1));
            bulb.SetColor("_EmissiveColor", new Color(0, 0, 0, 1));
            //bulb.SetFloat("_EmissiveIntensity", 0);
            flashlight.enabled = false;
        }
    }


    public void TriggerDown(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger Pulled");
        if (lightOn)
        {
            flashlight.enabled = false;
            lightOn = false;
            return;
        }
        else
        {
            if(batteryPower > 0)
            {
                flashlight.enabled = true;
                lightOn = true;
            }
            return;
        }
    }
}
