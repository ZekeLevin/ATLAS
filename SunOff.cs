using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunOff : MonoBehaviour
{
    public Light sunlight;
    private void OnTriggerEnter(Collider other)
    {
        sunlight.enabled = false;
    }
    private void OnTriggerExit(Collider other)
    {
        sunlight.enabled = true;
    }
}
