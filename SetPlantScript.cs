using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlantScript : MonoBehaviour
{
    public GameObject plant;
    public static bool plantReplaced;
    private float delay;
    private bool set;
    // Start is called before the first frame update
    void Start()
    {
        set = false;
        StartCoroutine(NotSet());
    }


    private void OnTriggerStay(Collider other)
    {
        if (set == false && other.gameObject.layer == 13)
        {
            plant.transform.position = other.transform.position;
            plant.transform.rotation = other.transform.rotation;
            plant.transform.Rotate(-90, 180, 0);
            plant.transform.parent = other.gameObject.transform;
            other.gameObject.layer = 14;
            set = true;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (set == true && other.gameObject.layer == 14 && VentilationScript.ventilated)
        {
            GagueScript.drainLife = false;
            GagueScript.lifeLevel = GagueScript.lifeTimer;
            StartCoroutine(DelayDrain());
            plantReplaced = true;
        }
    }


    IEnumerator DelayDrain()
    {
        yield return new WaitForSeconds(delay);
        GagueScript.drainLife = true;
    }

    IEnumerator NotSet()
    {
        yield return new WaitForSeconds(3);
        if (set == false)
        {
            Debug.Log("Plant is not set. Please contact the gallery monitor to reset the scene");
        }
    }
}
