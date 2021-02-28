using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterScript : MonoBehaviour
{
    
    public bool isTopShutter = false;
    public Light ambientLight;

    private string inputValue;
    private int outputValue;
    private float conversionValue;
    private float rotationValue;
    private bool isNumber;
    

    void Start()
    {
        //OpenShutters();
    }

    void FixedUpdate()
    {

        inputValue = Input.inputString;
        if (inputValue.Length < 3)
        {
            inputValue = "";
        }

        if (inputValue != "")
        {
            isNumber = int.TryParse(inputValue, out outputValue);
            if (isNumber == true)
            {
                conversionValue = outputValue; //convert output value to float

                rotationValue = conversionValue / 685;
                Debug.Log("Input: " + inputValue);
                Debug.Log("Rotation: " + rotationValue);


                if (isTopShutter == false)
                {
                    transform.localEulerAngles = new Vector3(-.5f, -150f, rotationValue * 120 - 25);
                }
                if (isTopShutter == true)
                {
                    transform.localEulerAngles = new Vector3(0, 0,rotationValue*90);
                }

            }
        }


        if(Input.GetKeyDown(KeyCode.O))
        {
            OpenShutters();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseShutters();
        }

        //control the ambient light
        if (isTopShutter)
        {
            ambientLight.intensity = (transform.localEulerAngles.z / 20) +1f;
        }

    }

    void OpenShutters()
    {
        if (isTopShutter == false)
        {
            transform.localEulerAngles = new Vector3(-.5f, -150f, 60);
        }

        if(isTopShutter == true)
        { 
            transform.localEulerAngles = new Vector3(0, 0, 90);
        }
    }

    void CloseShutters()
    {
        if (isTopShutter == false)
        {
            transform.localEulerAngles = new Vector3(-.5f, -150f, - 25);
        }
        if (isTopShutter == true)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
        }
        
    }
}
