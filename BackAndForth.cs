using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour
{
    public float distance;
    public float speed;
    private Vector3 startPosition;
    private bool forth = true;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(forth == true)
        {
            transform.Translate(-speed,0,0);
            if(transform.position.x <= startPosition.x - distance)
            {
                
                forth = false;
            }
        }
        else
        {
            transform.Translate(speed,0,0);
            if(transform.position.x >= startPosition.x + distance)
            {
                forth = true;
            }
        }
    }
}
