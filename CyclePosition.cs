using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclePosition : MonoBehaviour
{
    public Material mat;
    public GameObject rotationController;
    
    private float inputX;
    // Start is called before the first frame update
    void Start()
    {
        //mat = gameObject.GetComponent<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        
        inputX = (rotationController.transform.eulerAngles.y/360) - (gameObject.transform.eulerAngles.y/360);
        //Debug.Log(inputX);
        mat.SetVector("_LightingOffset",new Vector2 (inputX,0));
    }
}
