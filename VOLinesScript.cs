using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VOLinesScript : MonoBehaviour
{
    public AudioClip[] lines;
    public AudioClip[] failStateLines;
    public AudioClip[] waitingLines;
    public AudioSource radio;
    public AudioSource whiteNoise;
    public static int voIndex;
    private AudioClip lastClip;
    // Start is called before the first frame update
    void Start()
    {
        voIndex = 0;
        lastClip = lines[0];
        StartCoroutine(PlayFirstClip(45));
        //StartCoroutine(PlayStatic(30, .8f, .06f));
    }

    // Update is called once per frame
    void Update()
    {
        if(lines[voIndex] != lastClip)
        {
            StartCoroutine(PlayClip(lines[voIndex]));
        }
        
        lastClip = lines[voIndex];
    }

    IEnumerator PlayFirstClip(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        radio.clip = lines[0];
        whiteNoise.volume = .7f;
        yield return new WaitForSeconds(.5f);
        radio.Play();
        yield return new WaitForSeconds(lines[0].length);
        yield return new WaitForSeconds(1f);
        whiteNoise.volume = 0;

        yield return new WaitForSeconds(16);
        radio.clip = failStateLines[0];
        whiteNoise.volume = .7f;
        yield return new WaitForSeconds(.5f);
        radio.Play();
        yield return new WaitForSeconds(failStateLines[0].length);
        yield return new WaitForSeconds(1f);
        whiteNoise.volume = 0;
        

    }



    IEnumerator PlayClip(AudioClip line)
    {
        radio.clip = line;
        whiteNoise.volume = .7f;
        yield return new WaitForSeconds(.5f);
        radio.Play();
        yield return new WaitForSeconds(line.length);
        yield return new WaitForSeconds(1f);
        whiteNoise.volume = 0;

    }

    /*

    IEnumerator PlayStatic(float delay, float maxVolume, float gainSpeed)
    {
        whiteNoise.volume = 0;
        float i =0;
        while(i<delay)
        {
            i+=Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        if(i >= delay)
        {
            while(whiteNoise.volume < maxVolume)
            {
                whiteNoise.volume += gainSpeed;
                yield return new WaitForEndOfFrame();
            }
        }
    }
    */
}
