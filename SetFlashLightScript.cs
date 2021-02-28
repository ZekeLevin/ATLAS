using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFlashLightScript : MonoBehaviour
{
    public GameObject flashlight;
    private bool set;
    // Start is called before the first frame update
    void Start()
    {
        set = false;
        StartCoroutine(NotSet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(set == false && other.gameObject.layer == 13)
        {
            flashlight.transform.position = other.transform.position;
            flashlight.transform.rotation = other.transform.rotation;
            flashlight.transform.Rotate(transform.up, 180);
            flashlight.transform.parent = other.gameObject.transform;
            other.GetComponent<Rigidbody>().detectCollisions = false;
            other.enabled = false;
            gameObject.SetActive(false);
            set = true;
        }
    }

    IEnumerator NotSet()
    {
        yield return new WaitForSeconds(3);
        if(set == false)
        {
            Debug.Log("Flashlight is not set. Please contact the gallery monitor to reset the scene");
        }
    }
}
