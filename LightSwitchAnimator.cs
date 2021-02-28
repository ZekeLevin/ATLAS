using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitchAnimator : MonoBehaviour
{
    private bool switchedOn;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(-10, 0, 0);
        switchedOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(switchedOn)
            {
                gameObject.transform.Rotate(20, 0, 0);
                switchedOn = false;
                return;
            }
            else
            {
                gameObject.transform.Rotate(-20, 0, 0);
                switchedOn = true;
            }

        }
        
    }
}
