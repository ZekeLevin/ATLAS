using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    public Animator animator;
    private bool ClawAlreadyClosed;
    private bool grabbed = false;
    private Vector3 baseRotation;
    private Vector3 rotationOffset = new Vector3(0, 0, 0);
    private bool positionSet = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ClawState: " + animator.GetBool("ClawClosed"));
    }


    void OnTriggerEnter(Collider other)
    {
        if (animator.GetBool("ClawClosed") == true)
        {
            ClawAlreadyClosed = true;
        }
        else
        {
            ClawAlreadyClosed = false;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 12)
        {
            if (animator.GetBool("ClawClosed") == true && ClawAlreadyClosed == false)
            {

                if (other.gameObject.layer == 10)
                {
                    if (positionSet == false)
                    {
                        baseRotation = other.transform.eulerAngles;
                        other.transform.LookAt(gameObject.transform.position);
                        rotationOffset = baseRotation - other.transform.eulerAngles;
                        Debug.Log("rotation offset: " + rotationOffset);
                        positionSet = true;
                    }
                    other.transform.LookAt(gameObject.transform.position);
                    other.transform.eulerAngles = other.transform.eulerAngles + rotationOffset;


                }
                else
                {
                    other.transform.parent = gameObject.transform;
                }

                other.GetComponent<Rigidbody>().isKinematic = true;
                grabbed = true;
            }
            if (animator.GetBool("ClawClosed") == false && grabbed == true)
            {
                if(gameObject.transform.childCount > 0)
                {
                    for(int i =0; i<gameObject.transform.childCount; i++)
                    {
                        gameObject.transform.GetChild(i).transform.parent = null;
                    }
                    //other.transform.parent = null;
                    other.GetComponent<Rigidbody>().isKinematic = false;
                }
                ClawAlreadyClosed = false;
                positionSet = false;
                grabbed = false;
            }
        }
    }
}
