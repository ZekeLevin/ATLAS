using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAnimationScript : MonoBehaviour
{
    public GameObject redJoystick;
    public GameObject blackJoystick;
    public static bool inUse;
    // Start is called before the first frame update
    void Start()
    {
        redJoystick.transform.localEulerAngles = new Vector3(0,0,0);
        blackJoystick.transform.localEulerAngles = new Vector3(0,0,0);
        //inUse = false;
    }

    // Update is called once per frame
    void Update()
    {
        //using rotate allows for multiple rotations
        
        //rotate it forward on key down

        //red Joystick
        if(Input.GetKeyDown(KeyCode.W))
        {
            redJoystick.transform.Rotate(10,0,0);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            redJoystick.transform.Rotate(-10,0,0);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            redJoystick.transform.Rotate(0,0,10);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            redJoystick.transform.Rotate(0,0,-10);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());

        }

        //black joystick
        if (Input.GetKeyDown(KeyCode.I))
        {
            blackJoystick.transform.Rotate(10, 0, 0);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            blackJoystick.transform.Rotate(-10, 0, 0);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());

        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            blackJoystick.transform.Rotate(0, 0, 10);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            blackJoystick.transform.Rotate(0, 0, -10);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());

        }

        //rotate it back on key up

        //red Joystick
        if (Input.GetKeyUp(KeyCode.W))
        {
            redJoystick.transform.Rotate(-10,0,0);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());
        }
        if(Input.GetKeyUp(KeyCode.S))
        {
            redJoystick.transform.Rotate(10,0,0);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());
        }
        if(Input.GetKeyUp(KeyCode.A))
        {
            redJoystick.transform.Rotate(0,0,-10);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());
        }
        if(Input.GetKeyUp(KeyCode.D))
        {
            redJoystick.transform.Rotate(0,0,10);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());
        }

        //black Joystick
        if (Input.GetKeyUp(KeyCode.I))
        {
            blackJoystick.transform.Rotate(-10, 0, 0);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());
        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            blackJoystick.transform.Rotate(10, 0, 0);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            blackJoystick.transform.Rotate(0, 0, -10);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            blackJoystick.transform.Rotate(0, 0, 10);
            StopCoroutine(ResetJoystick());
            StartCoroutine(ResetJoystick());
        }

        /*if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            inUse = true;
        }
        else
        {
            redJoystick.transform.localEulerAngles = new Vector3(0, 0, 0);
            blackJoystick.transform.localEulerAngles = new Vector3(0, 0, 0);
            inUse = false;
        }
        */

    }


    IEnumerator ResetJoystick()
    {
        yield return new WaitForSeconds(1);
        redJoystick.transform.localEulerAngles = new Vector3(0, 0, 0);
        blackJoystick.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
