using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentilationScript : MonoBehaviour
{
    public static bool ventilated;
    public static bool venting;
    public AudioSource ventilationSound;
    public AudioClip ventilationClip;
    public AudioClip errorClip;
    private bool early;
    // Start is called before the first frame update
    void Start()
    {
        early = false;
    }

    private void Update()
    {
        if(venting)
        {
            GagueScript.energyLevel -= Time.deltaTime * 2f * GagueScript.drainModifier;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 14)
        {
            venting = true;
            StartCoroutine(Ventilate());
            early = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 14 && ventilated == false)
        {
            early = true;
            venting = false;
            ventilationSound.clip = errorClip;
            ventilationSound.Play();
            ventilationSound.loop = false;
        }
    }

    IEnumerator Ventilate()
    {
        ventilationSound.clip = ventilationClip;
        ventilationSound.Play();
        ventilationSound.loop = false;
        yield return new WaitForSeconds(ventilationClip.length);
        if(early == false)
        {
            ventilated = true;
            venting = false;
        }

    }
}
