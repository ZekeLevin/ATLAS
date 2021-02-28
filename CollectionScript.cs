using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionScript : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 11  && other.gameObject.GetComponent<Rigidbody>().isKinematic == false)
        {
            other.gameObject.transform.position = transform.position;
            other.gameObject.transform.rotation = transform.rotation;
            other.enabled = false;
            gameObject.SetActive(false);
            NarrativeScript.solarCounter++;
            GagueScript.drainModifier -= .1f;
        }
    }
}
