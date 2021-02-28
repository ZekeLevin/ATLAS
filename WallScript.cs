using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public static bool inWall;
    
    void Start()
    {
        inWall = false;
    }
    void OnTriggerEnter(Collider other)
    {
        inWall = true;
    }
    void OnTriggerStay(Collider other)
    {
        other.gameObject.transform.parent.Translate(0,0,-.1f);
    }

    void OnTriggerExit(Collider other)
    {
        inWall = false;
    }

}
