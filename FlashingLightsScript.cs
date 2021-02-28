using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingLightsScript : MonoBehaviour
{
    public GameObject[] lights;
    public AudioSource pa;
    public AudioClip alarm;
    public AudioClip warning;
    public Image veil;

    private void Start()
    {
    }
    public IEnumerator FlashLights(int repeat, float delay)
    {
        int i = 0;
        while (i < repeat)
        {
            foreach (GameObject light in lights)
            {
                light.SetActive(true);
            }

            yield return new WaitForSeconds(delay);

            foreach (GameObject light in lights)
            {
                light.SetActive(false);
            }
            yield return new WaitForSeconds(delay+.5f);
            i++;
        }

    }

    public IEnumerator Alarm(int repeat)
    {
        
        int i = 0;
        while(i < repeat)
        {
            pa.clip = alarm;
            pa.Play();
            yield return new WaitForSeconds(3.5f);
            pa.clip = warning;
            pa.Play();
            yield return new WaitForSeconds(4f);
            i++;
        }

        StartCoroutine(KillPlayer());
    }
    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(2);
        veil.CrossFadeAlpha(.95f, 12, true);
        while(GagueScript.lifeLevel <=0)
        {
            yield return new WaitForEndOfFrame();
        }
        veil.CrossFadeAlpha(0, 8, false);
    }
}
