using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamMovement : MonoBehaviour
{
    public float sensitivity = 1;
    private float horizontal;
    private float vertical;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal += sensitivity * Input.GetAxis("Mouse X");
        vertical -= sensitivity * Input.GetAxis("Mouse Y");
     transform.eulerAngles = new Vector3(vertical,horizontal, 0f); 
     //Debug.Log(Input.GetAxis("Mouse Y") + "Mouse Y" + Input.GetAxis("Mouse X")+ "Mouse X");
    }
}
