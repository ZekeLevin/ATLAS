using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    [Range(0,1)]
    public float speed;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(WallScript.inWall == false)
        {
        //horizontal
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.forward*speed, Space.Self);
        }
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.left*speed, Space.Self);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.back*speed, Space.Self);
        }
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.right*speed, Space.Self);
        }

        //verical
        if(Input.GetKey(KeyCode.I))
        {
            transform.Translate(Vector3.up*speed, Space.World);
        }
        if(Input.GetKey(KeyCode.K))
        {
            transform.Translate(Vector3.down*speed, Space.World);
        }

        //Rotation
        if(Input.GetKey(KeyCode.J))
        {
            transform.Rotate(0,-1,0);
        }
        if(Input.GetKey(KeyCode.L))
        {
            transform.Rotate(0,1,0);
        }
        }
    }
}
