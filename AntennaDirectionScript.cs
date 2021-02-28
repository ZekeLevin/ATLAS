using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntennaDirectionScript : MonoBehaviour
{
    public static bool aligned;
    // Start is called before the first frame update
    void Start()
    {
        aligned = false;
    }

    private void OnTriggerStay(Collider other)
    {
        aligned = true;
    }
}
