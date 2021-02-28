using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeScript : MonoBehaviour
{
    //UI-----------------
    public Image veil;
    public Text objectivesText;
    public string[] objectivesList;
    public string[] credits;
    private int objectivesIndex;
    public GameObject endTitleCard;


    //Environment------------
    public FlashingLightsScript lightFlashScript;
    public GameObject topShutter;
    public GameObject supplyPod;

    //Audio-----------
    public AudioClip alarm;
    public AudioClip collisionWarning;
    public AudioSource pa;
    public AudioClip objectivesBing;
    public AudioClip[] lines;
    public AudioClip[] failStateLines;
    public AudioClip[] waitingLines;
    public AudioSource radio;
    public AudioSource whiteNoise;
    public static int voIndex;
    public AudioClip music;
    public AudioClip rechargeLine;
    public AudioClip rechargeSfx;
    private AudioClip lastClip;

    //Gameplay------------------
    public static int solarCounter;
    private int currentSolarCount;
    public string[] solarCount;
    public static bool recharged;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FirstAlarm(3));
        StartCoroutine(lightFlashScript.FlashLights(16, .6f));
        StartCoroutine(LiftVeil(12));//wait to lift veil
        StartCoroutine(PlayFirstClip(45));
        //StartCoroutine(Endgame());

        recharged = false;

        //set indexes to 0
        voIndex = 0;
        objectivesIndex = 0;
        lastClip = lines[0];
        objectivesText.CrossFadeAlpha(0,0,false);
        solarCounter = 0;
        currentSolarCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Audio----------
        /* if(lines[voIndex] != lastClip)
         {
             StartCoroutine(PlayClip(lines[voIndex]));
         }

         lastClip = lines[voIndex];
         */

        Debug.Log(MorseScript.output);

        if(solarCounter != currentSolarCount)
        {
            StartCoroutine(UpdateSolarCount());
            currentSolarCount = solarCounter;
            
        }

        if(GagueScript.energyLevel <=0)
        {
            StartCoroutine(RechargeBatteries());
        }
    }




    //UI__________________________________________________________________________________________
    public IEnumerator LiftVeil(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("Ready!");
        veil.CrossFadeAlpha(0,8f,false);
    }

    public IEnumerator ShowNextObjective()
    {
        objectivesText.text = objectivesList[objectivesIndex];
        objectivesText.CrossFadeAlpha(.8f,4f,false);
        yield return new WaitForSeconds(.2f);
        pa.clip = objectivesBing;
        pa.volume = .3f;
        pa.Play();
        yield return new WaitForSeconds(8);
        objectivesText.CrossFadeAlpha(0,4,false);
        yield return new WaitForSeconds(4);
        pa.volume = 1;
        objectivesIndex++;
    }

    public IEnumerator UpdateSolarCount()
    {
        objectivesText.text = solarCount[solarCounter -1];
        objectivesText.CrossFadeAlpha(.8f, 4f, false);
        yield return new WaitForSeconds(.2f);
        pa.clip = objectivesBing;
        pa.volume = .3f;
        pa.Play();
        yield return new WaitForSeconds(8);
        objectivesText.CrossFadeAlpha(0, 4, false);
        yield return new WaitForSeconds(4);
        pa.volume = 1;
    }

    void ShowLastObjective()
    {
        objectivesIndex--;
        StartCoroutine(ShowNextObjective());
    }


    //Audio________________________________________________________________________________________
    IEnumerator FirstAlarm(int repeat)
    {
        yield return new WaitForSeconds(4);
        
        int i = 0;
        while(i < repeat)
        {
            pa.clip = alarm;
            pa.Play();
            yield return new WaitForSeconds(3.5f);
            pa.clip = collisionWarning;
            pa.Play();
            yield return new WaitForSeconds(3f);
            i++;
        }
        
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

    //Gameplay______________________________________________________________________________

    IEnumerator RechargeBatteries()
    {
        /*pa.clip = rechargeLine;
        pa.Play();
        pa.loop = false;
        yield return new WaitForSeconds(rechargeLine.length + .3f);
        pa.clip = rechargeSfx;
        pa.Play();
        pa.loop = true;
        yield return new WaitForSeconds(rechargeSfx.length + 1);
        */
        yield return new WaitForSeconds(30);
        GagueScript.energyLevel = 400;
        recharged = true;
    }




    //Narrative___________________________________________________________________________

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

        yield return new WaitForSeconds(3);
        StartCoroutine(ShowNextObjective());

        StartCoroutine(WaitForRadio());

    }
    IEnumerator WaitForRadio()
    {
        Vector3 RadStartPos = radio.transform.position;
        yield return new WaitForSeconds(2);
       float i = 0;
        while(Vector3.Distance(radio.transform.position, RadStartPos) <= .1)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Skip!");
                break;
            }
            
            yield return new WaitForEndOfFrame();
            i += Time.deltaTime;
            if( i> 30)
            {
                radio.clip = failStateLines[0];
                whiteNoise.volume = .7f;
                yield return new WaitForSeconds(.5f);
                radio.Play();
                yield return new WaitForSeconds(failStateLines[0].length);
                yield return new WaitForSeconds(1f);
                whiteNoise.volume = 0;
                yield return new WaitForSeconds(failStateLines[0].length + 2);
                ShowLastObjective();
                i = 0;
            }
        }
        Debug.Log("NEXT");
        yield return new WaitForSeconds(1);
        StartCoroutine(PlayClip(lines[1]));
        yield return new WaitForSeconds(lines[1].length + 2f);
        StartCoroutine(ShowNextObjective());
        StartCoroutine(WaitForMorse());
    }

    IEnumerator WaitForMorse()
    {

        float i = 0;
        while (MorseScript.output == "")
        {
            Debug.Log("Stuck");
            if (Input.GetKeyDown(KeyCode.Space))
                MorseScript.output = "Help";
            yield return new WaitForEndOfFrame();
            i += Time.deltaTime;
            if(i>45)
            {
                ShowLastObjective();
                i = 0;
            }
        }
        if(MorseScript.output != "")
        {
            StartCoroutine(PlayClip(lines[2]));
            yield return new WaitForSeconds(lines[2].length + 2);
            CheckIfShuttersOpen();
        }

        

    }

    void CheckIfShuttersOpen()
    {
        if(topShutter.transform.eulerAngles.z < 45)
        {
            StartCoroutine(WaitForShutters());
        }
        else
        {
            StartCoroutine(IntroduceArm());
            objectivesIndex++;
        }
    }

    IEnumerator WaitForShutters()
    {
        StartCoroutine(PlayClip(lines[3]));
        yield return new WaitForSeconds(lines[3].length + 2);
        StartCoroutine(ShowNextObjective());
        float i = 0;
        while(topShutter.transform.eulerAngles.z<45)
        {
            if (Input.GetKey(KeyCode.Space))
                break;
            yield return new WaitForEndOfFrame();
            i += Time.deltaTime;
            if(i > 45)
            {
                ShowLastObjective();
                i = 0;
            }
        }
        StartCoroutine(IntroduceArm());
        
    }

    IEnumerator IntroduceArm()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(PlayClip(lines[4]));
        yield return new WaitForSeconds(lines[4].length + 2);
        StartCoroutine(ShowNextObjective());

        float i = 0;
        while (AntennaDirectionScript.aligned == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                break;
            yield return new WaitForEndOfFrame();
            i += Time.deltaTime;
            if(i>45)
            {
                ShowLastObjective();
                i = 0;
            }
        }

        yield return new WaitForSeconds(.2f);
        StartCoroutine(PlayClip(lines[5]));
        yield return new WaitForSeconds(lines[5].length + 17);
        StartCoroutine(CheckLifeSupport());

    }

    IEnumerator CheckLifeSupport()
    {
        StartCoroutine(PlayClip(lines[6]));
        MorseScript.output = "";
        yield return new WaitForSeconds(lines[6].length + 2);
        StartCoroutine(ShowNextObjective());
        //yield return new WaitForSeconds(2);
        while (MorseScript.output != "GREEN   " && MorseScript.output != "GREEN_  " && MorseScript.output != "RED     " && MorseScript.output != "RED_    ")
        {
            if (Input.GetKey(KeyCode.Space))
                MorseScript.output = "RED     ";
            string oldOutput = MorseScript.output;
            yield return new WaitForEndOfFrame();
            if (MorseScript.output == "GREEN   " || MorseScript.output == "GREEN_  ")
            {
                yield return new WaitForSeconds(2);
                StartCoroutine(PlayClip(lines[7]));
                yield return new WaitForSeconds(lines[7].length);
                objectivesIndex += 3;
                StartCoroutine(RepairSolar());
                break;
            }

            
            if (MorseScript.output == "RED     " || MorseScript.output == "RED_    " || Input.GetKeyDown(KeyCode.Space))
            {
                SetPlantScript.plantReplaced = false;
                VentilationScript.venting = false;
                yield return new WaitForSeconds(2);
                StartCoroutine(PlayClip(lines[9]));
                yield return new WaitForSeconds(lines[9].length + 2);
                StartCoroutine(ShowNextObjective());
                float i = 0;
                while (VentilationScript.venting == false)
                {
                    if (Input.GetKey(KeyCode.Space))
                        break;
                    yield return new WaitForEndOfFrame();
                    i += Time.deltaTime;
                    if (i > 45)
                    {
                        ShowLastObjective();
                        i = 0;
                    }
                }
                yield return new WaitForSeconds(.6f);
                StartCoroutine(PlayClip(lines[10]));
                yield return new WaitForSeconds(lines[10].length + 2);
                StartCoroutine(ShowNextObjective());
                while (VentilationScript.ventilated == false)
                {
                    if (Input.GetKey(KeyCode.Space))
                        break;
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(.5f);
                StartCoroutine(ShowNextObjective());
                while (SetPlantScript.plantReplaced == false)
                {
                    if (Input.GetKey(KeyCode.Space))
                        break;
                    yield return new WaitForEndOfFrame();
                }
                yield return new WaitForSeconds(1);
                StartCoroutine(PlayClip(lines[11]));
                yield return new WaitForSeconds(lines[11].length);

                yield return new WaitForSeconds(20);
                StartCoroutine(RepairSolar());

                MorseScript.output = "";
                break;
            }
            if (MorseScript.output != oldOutput)
            {
                yield return new WaitForSeconds(2);
                StartCoroutine(PlayClip(lines[8]));
                yield return new WaitForSeconds(lines[8].length + 2);
                ShowLastObjective();
            }
        }

    }


    IEnumerator RepairSolar()
    {
        StartCoroutine(PlayClip(lines[12]));
        GagueScript.startDraining = true;
        yield return new WaitForSeconds(lines[12].length + 2);
        StartCoroutine(ShowNextObjective());
        currentSolarCount = solarCounter;
        int firstSolarCount = currentSolarCount;
        float i = 0;
        while(solarCounter == firstSolarCount)
        {
            if (Input.GetKey(KeyCode.Space))
                break;
            i += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if(i > 45)
            {
                ShowLastObjective();
                i = 0;
            }
        }
        yield return new WaitForSeconds(2);
        while(currentSolarCount <6)
        {
            if (Input.GetKey(KeyCode.Space))
                break;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(CallTheRussians());
    }


    IEnumerator CallTheRussians()
    {
        StartCoroutine(PlayClip(lines[13]));
        yield return new WaitForSeconds(lines[13].length + 2);
        StartCoroutine(ShowNextObjective());
        float i = 0;
        while (MorseScript.output != "SOS     " && MorseScript.output != "SOS_    ")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                break;
            }
            yield return new WaitForEndOfFrame();
            i += Time.deltaTime;
            if(i>45)
            {
                ShowLastObjective();
                i = 0;
            }
        }
        
        yield return new WaitForSeconds(4);
        StartCoroutine(PlayClip(lines[14]));
        yield return new WaitForSeconds(lines[14].length + 2);
        StartCoroutine(ShowNextObjective());
        StartCoroutine(Endgame());
        
    }

    IEnumerator Endgame()
    {
        yield return new WaitForSeconds(60);

        //script the supply pod spawning in and moving into position
        yield return new WaitForEndOfFrame();
        pa.clip = music;
        pa.Play();
        pa.volume = .3f;
        pa.loop = false;
        StartCoroutine(SupplyPod());

       

    }

    IEnumerator SupplyPod()
    {
        yield return new WaitForEndOfFrame();
        GameObject suPod = Instantiate(supplyPod, new Vector3(-10, 30, 4), Quaternion.Euler(20, 15, -140));
        while (Vector3.Distance(suPod.transform.position, new Vector3(-8.66f, 2.5f, 8f)) > 1)
        {
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(suPod.transform.position, new Vector3(-8.66f, 2.5f, 8f)) < 2f)
            {
                if (Vector3.Distance(suPod.transform.position, new Vector3(-8.66f, 2.5f, 8f)) < 1.2f)
                {
                    suPod.transform.position = Vector3.MoveTowards(suPod.transform.position, new Vector3(-8.66f, 2.5f, 8f), .004f);
                }
                else
                {
                    suPod.transform.position = Vector3.MoveTowards(suPod.transform.position, new Vector3(-8.66f, 2.5f, 8f), .008f);
                }
            }
            else
            {
                suPod.transform.position = Vector3.MoveTowards(suPod.transform.position, new Vector3(-8.66f, 2.5f, 8f), .01f);
            }
            //suPod.transform.Rotate(Vector3.up, .2f);
        }
        Debug.Log("1st Position");
        yield return new WaitForSeconds(3);
        while (suPod.transform.eulerAngles.z < 270)
        {
            Debug.Log(suPod.transform.eulerAngles);
            yield return new WaitForEndOfFrame();
            suPod.transform.Rotate(new Vector3(0, 0, .3f), Space.Self);
        }
        Debug.Log("2nd Position");
        yield return new WaitForSeconds(4);
        while (Vector3.Distance(suPod.transform.position, new Vector3(-.5f, 2.5f, 5)) > 1)
        {
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(suPod.transform.position, new Vector3(-.5f, 2.5f, 5)) < 2f)
            {
                if (Vector3.Distance(suPod.transform.position, new Vector3(-.5f, 2.5f, 5)) < 1.2f)
                {
                    suPod.transform.position = Vector3.MoveTowards(suPod.transform.position, new Vector3(-.5f, 2.5f, 5), .004f);
                }
                else
                {
                    suPod.transform.position = Vector3.MoveTowards(suPod.transform.position, new Vector3(-.5f, 2.5f, 5), .008f);
                }
            }
            else
            {
                suPod.transform.position = Vector3.MoveTowards(suPod.transform.position, new Vector3(-.5f, 2.5f, 5), .01f);
            }
        }
        Debug.Log("3rd position");

        yield return new WaitForSeconds(1.5f);
        //show next objective but its muted
        StartCoroutine(MutedObjective());


        float i = 0;
        while (SupplyPodScript.gameOver == false)
        {
            if (Input.GetKey(KeyCode.Space))
                break;
            yield return new WaitForEndOfFrame();
            i += Time.deltaTime;
            if (i > 45)
            {
                //show last objective but its muted
                objectivesIndex--;
                StartCoroutine(MutedObjective());
                i = 0;
            }
        }

        yield return new WaitForSeconds(1);
        while (pa.volume > .1)
        {
            yield return new WaitForEndOfFrame();
            pa.volume -= .003f;
        }
        yield return new WaitForSeconds(.3f);
        radio.spatialBlend = 0;
        StartCoroutine(PlayClip(lines[15]));
        yield return new WaitForSeconds(lines[15].length + 4);
        veil.CrossFadeAlpha(1, 15, false);
        StartCoroutine(EndCredits());
        yield return new WaitForSeconds(.4f);
        while (pa.volume < .3)
        {
            yield return new WaitForEndOfFrame();
            pa.volume += .001f;
        }
        
    }

    IEnumerator MutedObjective()
    {
        objectivesText.text = objectivesList[objectivesIndex];
        objectivesText.CrossFadeAlpha(.8f, 4f, false);
        yield return new WaitForSeconds(8);
        objectivesText.CrossFadeAlpha(0, 4, false);
        yield return new WaitForSeconds(4);
        objectivesIndex++;
    }
    IEnumerator EndCredits()
    {
        yield return new WaitForSeconds(20);
        int creditIndex = 0;
        while(creditIndex < credits.Length)
        {
            objectivesText.text = credits[creditIndex];
            objectivesText.CrossFadeAlpha(.8f, 4f, false);
            yield return new WaitForSeconds(8);
            objectivesText.CrossFadeAlpha(0, 4, false);
            yield return new WaitForSeconds(4.5f);
            creditIndex++;
        }
        yield return new WaitForSeconds(1.6f);
        endTitleCard.SetActive(true);
        yield return new WaitForSeconds(8);
        while (pa.volume > 0)
        {
            yield return new WaitForEndOfFrame();
            pa.volume -= .001f;
        }


    }
    //End Game

}
