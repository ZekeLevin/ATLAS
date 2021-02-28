using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAnimationScript : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(animator.GetBool("ClawClosed"));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(animator.GetBool("ClawClosed") == true)
            {
                animator.SetBool("ClawClosed", false);
               Debug.Log(animator.GetBool("ClawClosed"));
                return;
            }
            if(animator.GetBool("ClawClosed") == false)
            {
                animator.SetBool("ClawClosed", true);
                Debug.Log(animator.GetBool("ClawClosed"));
                return;
            }

           
           
        }

    }
}
