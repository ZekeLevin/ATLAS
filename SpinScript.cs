using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public bool local = false;
    public Vector3 spinSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(local == true)
        {
            transform.Rotate(spinSpeed.x,spinSpeed.y,spinSpeed.z, Space.Self);
        }
        else
        {
        transform.Rotate(spinSpeed.x,spinSpeed.y,spinSpeed.z, Space.World);
        }
    }
}
