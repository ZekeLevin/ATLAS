using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkingUniverse : MonoBehaviour
{
    public Camera cam;
    public float speed = .17f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(Shrink());
        }
    }

    IEnumerator Shrink()
    {
        while (cam.farClipPlane > 0)
        {
            cam.farClipPlane-=speed;
            yield return new WaitForEndOfFrame();
        }
    }
}
