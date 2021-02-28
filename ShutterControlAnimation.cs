using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterControlAnimation : MonoBehaviour
{
    public GameObject topShutter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float shutterRotation = topShutter.transform.localEulerAngles.z/90;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, shutterRotation*300);
    }
}
