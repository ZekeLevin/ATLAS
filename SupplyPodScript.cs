using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyPodScript : MonoBehaviour
{
    public static bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = null;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.parent != null)
        {
            gameOver = true;
        }
    }
}
