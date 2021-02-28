using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iKScript : MonoBehaviour
{
    public GameObject rootjoint;
    public GameObject elbowJoint;
    public GameObject target;
    public GameObject shutterBarrier;
    public GameObject[] screens;
    public float speed;
    private float maxDistance;
    private float currentDistance;
    private float upperArmLength;
    private float forearmLength;
    private Vector3 lookTransform;
    private Vector3 lastPosition;
    private Vector3 flubTransform;//used to test if the target will go out of range
    public static bool moduleOn = true;

    //UI
    public Image offPanel;
    public Image offBorder;
    public Text offText;

    public Image onPanel;
    public Image onBorder;
    public Text onText;




    void Start()
    {
        upperArmLength = Vector3.Distance(elbowJoint.transform.position, rootjoint.transform.position);
        forearmLength = Vector3.Distance(target.transform.position, elbowJoint.transform.position);
        maxDistance = Vector3.Distance(target.transform.position, rootjoint.transform.position);

        TurnOffModule();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (moduleOn)
            {
                TurnOffModule();
                return;
            }
            else
            {
                TurnOnModule();
                return;
            }
        }

        if(moduleOn)
        {
            GagueScript.energyLevel -= (Time.deltaTime * .5f) * GagueScript.drainModifier;
        }
       
        //complicated maths to make the claw move in all 4 quadrants
        float baseRot = elbowJoint.transform.eulerAngles.y;
        float targetRot = target.transform.eulerAngles.y;
        float jLimit = baseRot - 100;
        float lLimit = baseRot + 100;
        float combinedRot = baseRot - targetRot;


        //grab a position that works
        flubTransform = target.transform.position;

        //adjust root joint for target's distance
        currentDistance = Vector3.Distance(target.transform.position, rootjoint.transform.position); //grab current distance

        rootjoint.transform.LookAt(target.transform.position);
        lookTransform = rootjoint.transform.eulerAngles;    //grab orientation and set it to a stored value

        FindAngle();

        rootjoint.transform.eulerAngles = new Vector3(lookTransform.x, lookTransform.y + FindAngle(), lookTransform.z);//adjust y for distance and x and z for target's orientation




        //control Target Positon
        if (Input.GetKey(KeyCode.W) && moduleOn)
        {
            GagueScript.energyLevel -= (Time.deltaTime * 4)*GagueScript.drainModifier;
            flubTransform = flubTransform + Vector3.forward * speed;
        }
        if (Input.GetKey(KeyCode.A) && moduleOn)
        {
            GagueScript.energyLevel -= (Time.deltaTime * 4) * GagueScript.drainModifier;
            flubTransform = flubTransform + Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.S) && moduleOn)
        {
            GagueScript.energyLevel -= (Time.deltaTime * 4) * GagueScript.drainModifier;
            flubTransform = flubTransform + Vector3.back * speed;
        }
        if (Input.GetKey(KeyCode.D) && moduleOn)
        {
            GagueScript.energyLevel -= (Time.deltaTime * 4) * GagueScript.drainModifier;
            flubTransform = flubTransform + Vector3.right * speed;
        }
        if (Input.GetKey(KeyCode.I) && moduleOn)
        {
            GagueScript.energyLevel -= (Time.deltaTime * 4) * GagueScript.drainModifier;
            flubTransform = flubTransform + Vector3.up * speed;
        }
        if (Input.GetKey(KeyCode.K) && moduleOn)
        {
            GagueScript.energyLevel -= (Time.deltaTime * 4) * GagueScript.drainModifier;
            flubTransform = flubTransform + Vector3.down * speed;
        }


        float flubDistance = Vector3.Distance(flubTransform, rootjoint.transform.position);

        float barrierDistance = Vector3.Distance(flubTransform, shutterBarrier.transform.position);

        //check if joint is too far away
        if (flubDistance < maxDistance && barrierDistance >= 5.4)
        {
            target.transform.position = flubTransform;

            //Debug.Log("All Good");
        }
        if (flubDistance > maxDistance)
        {
            Debug.Log("Too Far!");
        }

        //rotate claw
        if (Input.GetKey(KeyCode.J) && FindJLimit() && moduleOn)
        {
            GagueScript.energyLevel -= (Time.deltaTime * 4) * GagueScript.drainModifier;
            target.transform.Rotate(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.L) && FindLLimit() && moduleOn)
        {
            GagueScript.energyLevel -= (Time.deltaTime * 4) * GagueScript.drainModifier;
            target.transform.Rotate(0, 1, 0);
        }

        //allign elbow joint
        elbowJoint.transform.LookAt(target.transform);


    }

    void TurnOffModule()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(false);
        }
        offText.color = new Color(offText.color.r, offText.color.g, offText.color.b, .3f);
        offPanel.color = new Color(offPanel.color.r, offPanel.color.g, offPanel.color.b, .3f);
        offBorder.color = new Color(offBorder.color.r, offBorder.color.g, offBorder.color.b, .3f);

        onText.color = new Color(onText.color.r, onText.color.g, onText.color.b, .01f);
        onPanel.color = new Color(onPanel.color.r, onPanel.color.g, onPanel.color.b, .01f);
        offBorder.color = new Color(onBorder.color.r, onBorder.color.g, onBorder.color.b, .01f);

        moduleOn = false;
    }

    void TurnOnModule()
    {
        foreach (GameObject screen in screens)
        {
            screen.SetActive(true);
        }

        offText.color = new Color(offText.color.r, offText.color.g, offText.color.b, .01f);
        offPanel.color = new Color(offPanel.color.r, offPanel.color.g, offPanel.color.b, .01f);
        offBorder.color = new Color(offBorder.color.r, offBorder.color.g, offBorder.color.b, .01f);

        onText.color = new Color(onText.color.r, onText.color.g, onText.color.b, .3f);
        onPanel.color = new Color(onPanel.color.r, onPanel.color.g, onPanel.color.b, .3f);
        offBorder.color = new Color(onBorder.color.r, onBorder.color.g, onBorder.color.b, .3f);

        moduleOn = true;
    }


    float FindAngle()
    {
        float angle = Mathf.Rad2Deg * Mathf.Acos((Mathf.Pow(upperArmLength, 2) + Mathf.Pow(currentDistance, 2) - Mathf.Pow(forearmLength, 2)) / (2 * upperArmLength * currentDistance));
        return angle;
    }


    bool FindJLimit()
    {
        float baseRot = elbowJoint.transform.eulerAngles.y;
        float targetRot = target.transform.eulerAngles.y;
        float jLimit = baseRot - 100;
        bool rotationCheck = false;


        if (jLimit < 0)
        {
            jLimit = 360 + jLimit;
            if (targetRot > jLimit || targetRot < baseRot + 180)
            {
                rotationCheck = true;
                return rotationCheck;
            }


        }
        else
        {

            if (targetRot > jLimit || targetRot < baseRot - 180)
            {
                rotationCheck = true;
                return rotationCheck;
            }

        }
        return rotationCheck;
    }
    bool FindLLimit()
    {
        float baseRot = elbowJoint.transform.eulerAngles.y;
        float targetRot = target.transform.eulerAngles.y;
        float lLimit = baseRot + 100;
        bool rotationCheck = false;

        if (lLimit > 360)
        {
            lLimit = lLimit - 360;
            if (targetRot < lLimit || targetRot > baseRot - 180)
            {
                rotationCheck = true;
                return rotationCheck;
            }
        }
        else
        {
            Debug.Log("TRot" + targetRot);
            Debug.Log(lLimit);
            if (targetRot < lLimit || targetRot > baseRot + 180)
            {
                rotationCheck = true;
                return rotationCheck;
            }
        }


        return rotationCheck;
    }
}
