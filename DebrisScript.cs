using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisScript : MonoBehaviour
{
    public Material[] materialList;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Material mat in materialList)
        {
            mat.SetFloat("_Clean", 1);
            mat.SetFloat("_Cracked", 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            CrackWindows();
        }
    }


    void CrackWindows()
    {
        foreach(Material mat in materialList)
        {
            if(mat.GetInt("_Clean") == 1)
            {
                mat.SetVector("_Offset1", new Vector4(Random.Range(0f,3f),Random.Range(0f,3f),0,0));
                mat.SetFloat("Rotation1", Random.Range(0,2));
                mat.SetInt("_Clean", 0);
            }
            else
            {
                mat.SetVector("_Offset2", new Vector4(Random.Range(0f,3f),Random.Range(0f,3f),0,0));
                mat.SetFloat("Rotation2", Random.Range(0,2));
                mat.SetInt("_Cracked", 1);
            }
        }
    }
}
