using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GagueScript : MonoBehaviour
{
    //UI
    public Text remainingPowerText;
    public Image greenBar;
    public Image yellowBar;
    public Image orangeBar;
    public Image redBar;
    
    //Power and Life levels
    public float energyTimer = 120;
    public static float energyLevel;
    public static float energyDrainRate =0;
    private float lastEnergyLevel;
    public static bool startDraining;

    public static float lifeTimer = 200;
    public static float lifeLevel;
    public static bool drainLife;
    public static float drainModifier;

    public FlashingLightsScript flashingLightsScript;

    //Life Support indicators
    public Material greenLight;
    public Material yellowLight;
    public Material redLight;

    //items to turn off if power reaches 0
    public GameObject[] screens;
    private bool flashing = false;

    // Start is called before the first frame update
    void Start()
    {
        drainModifier = 1;
        startDraining = false;
        lifeLevel = lifeTimer;
        drainLife = false;
        energyLevel = energyTimer;
        greenLight.SetFloat("_EmissiveIntensity", 0.01f);
        yellowLight.SetFloat("_EmissiveIntensity", 0.01f);
        redLight.SetFloat("_EmissiveIntensity", 0.01f);
        //sets to red
        greenLight.SetColor("_EmissiveColor", new Color(0, 0, 0, 1));
        yellowLight.SetColor("_EmissiveColor", new Color(0, 0, 0, 1));
        redLight.SetColor("_EmissiveColor", new Color(1, 0, 0, 1));
        energyDrainRate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Update UI
        Drain();
        
        //currentDrawText.text = "- " + (energyDrainRate*3000).ToString("N1") + " W";
        remainingPowerText.text = (energyLevel*.0003).ToString("N3") + " kWh";
        if (energyLevel > .5 * energyTimer)
        {
            remainingPowerText.color = new Color(0, 1f, 0, .3f);
            
        }
        if (energyLevel < .5f * energyTimer)
        {
            if (energyLevel < .25f * energyTimer)
            {
                remainingPowerText.color = new Color(1, 0, 0, .3f);
               /* if (energyLevel < .1 * energyTimer && flashing == false)
                {
                    StartCoroutine(flashingLightsScript.Alarm(3));
                    StartCoroutine(flashingLightsScript.FlashLights(14, .6f));
                    flashing = true;
                }*/
            }
            else
            {
                remainingPowerText.color = new Color(1, .92f, .0016f, .3f);
           
            }
        }
        /*
        if(energyDrainRate == 0)
        {
            greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .3f);
            yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .01f);
            orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .01f);
            redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .01f);
            //currentDrawText.color = new Color(0, 1f, 0, .3f);
        }
        if(energyDrainRate > 0)
        {
            if(energyDrainRate == 1)
            {
                greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .01f);
                yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .3f);
                orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .01f);
                redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .01f);

                
               
            }
            if (energyDrainRate == 2)
            {
                greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .01f);
                yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .01f);
                orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .3f);
                redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .01f);
            }
            else
            {
                greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .01f);
                yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .01f);
                orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .01f);
                redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .3f);
                //currentDrawText.color = new Color(1, .92f, .0016f, .3f);
            }
        }*/


       
        lastEnergyLevel = energyLevel;

        //reset if we haven't introduced solar yet
        if(startDraining == false)
        {
            energyLevel = energyTimer;
        }

        //life support

        if (drainLife == true)
        {
            lifeLevel -= Time.deltaTime;


            if (lifeLevel > .5 * lifeTimer)
            {
                flashing = false;
                //greenLight.SetFloat("_EmissiveIntensity", 1f);
                greenLight.SetColor("_EmissiveColor", new Color(0, 1f, 0, 0));
                yellowLight.SetColor("_EmissiveColor", new Color(0, 0, 0, 1));
                redLight.SetColor("_EmissiveColor", new Color(0, 0, 0, 1));
            }
            if (lifeLevel < .5f * lifeTimer)
            {
                if (lifeLevel < .25f * lifeTimer)
                {
                    greenLight.SetColor("_EmissiveColor", new Color(0, 0, 0, 1));
                    yellowLight.SetColor("_EmissiveColor", new Color(0, 0, 0, 1));
                    redLight.SetColor("_EmissiveColor", new Color(1, 0, 0, 1));
                    if (lifeLevel < .1 * lifeTimer && flashing == false)
                    {
                        StartCoroutine(flashingLightsScript.Alarm(3));
                        StartCoroutine(flashingLightsScript.FlashLights(14, .6f));
                        flashing = true;
                    }
                }
                else
                {
                    greenLight.SetColor("_EmissiveColor", new Color(0, 0, 0, 1));
                    yellowLight.SetColor("_EmissiveColor", new Color(1, .92f, .0016f, 1));
                    redLight.SetFloat("_EmissiveIntensity", 0.01f);
                }
            }
        }

    }

    void Drain()
    {
        if(TurnOffIntLights.lightsOn || iKScript.moduleOn)
        {
            if (TurnOffIntLights.lightsOn && iKScript.moduleOn)
            {
                if (VentilationScript.venting)
                {
                    greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .01f);
                    yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .01f);
                    orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .01f);
                    redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .3f);
                }
                else
                {
                    greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .01f);
                    yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .01f);
                    orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .3f);
                    redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .01f);
                }
            }
            else
            {
                if (VentilationScript.venting)
                {
                    greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .01f);
                    yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .01f);
                    orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .3f);
                    redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .01f);
                }
              
                else
                {
                    greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .01f);
                    yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .3f);
                    orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .01f);
                    redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .01f);
                }
            }
        }
        else 
        {
            greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .3f);
            yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .01f);
            orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .01f);
            redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .01f);
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            greenBar.color = new Color(greenBar.color.r, greenBar.color.g, greenBar.color.b, .01f);
            yellowBar.color = new Color(yellowBar.color.r, yellowBar.color.g, yellowBar.color.b, .01f);
            orangeBar.color = new Color(orangeBar.color.r, orangeBar.color.g, orangeBar.color.b, .01f);
            redBar.color = new Color(redBar.color.r, redBar.color.g, redBar.color.b, .8f);
        }
    }


}
