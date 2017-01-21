using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class music : MonoBehaviour
{

    public AudioSource[] basic;
    public AudioSource[] happy;
    public AudioSource[] sad;

    private music musicReference;

    private int moodCounter = 0;
    private bool moodImproving = true;
    private bool fadingIn = false;

    private int moodCounterMin;
    private int moodCounterMax;

    private float globalVolume = 0.25f;
    private float timeBetweenMoodUpdate = 1f;

    private GameObject[] playerObjects;
    private GameObject[] happyObjects;
    private GameObject[] sadObjects;

    List<Coroutine> fading = new List<Coroutine>();

    public music _Reference
    {
        get { return musicReference; }
        private set { }
    }

    public void Awake()
    {
        musicReference = this;
        moodCounterMin = -(sad.Length);
        moodCounterMax = happy.Length;
        muteAllSpecial();
        startMoodWave();
        setVolumeAll(globalVolume);

        playerObjects = GameObject.FindGameObjectsWithTag("Player");
        happyObjects = GameObject.FindGameObjectsWithTag("Happy");
        sadObjects = GameObject.FindGameObjectsWithTag("Sad");

        globalMoodChange("Happy");
    }

    public void Start()
    {
        
    }

    public void setVolumeAll(float intensity)
    {
        intensity = Mathf.Clamp01(intensity);

        for (int i = 0; i < basic.Length; i++)
        {
            basic[i].volume = 0;//intensity;
            basic[i].pitch = 1.15f;
        }

        for (int i = 0; i < happy.Length; i++)
        {
            happy[i].volume = intensity;
            happy[i].pitch = 1.25f;
        }

        for (int i = 0; i < sad.Length; i++)
        {
            sad[i].volume = intensity;
            sad[i].pitch = 0.8f;
        }
    }

    public void muteAllSpecial()
    {
        for (int i = 0; i < happy.Length; i++)
        {
            happy[i].mute = true;
        }

        for (int i = 0; i < sad.Length; i++)
        {
            sad[i].mute = true;
        }
    }

    public void playHappy(int intensity)
    {
        intensity = (int)Mathf.Clamp(intensity, 1, happy.Length);
        muteAllSpecial();
        for (int i = 0; i < intensity; i++)
        {
            happy[i].mute = false;
        }
        globalMoodChange("Happy");
    }

    public void playSad(int intensity)
    {
        intensity = (int)Mathf.Clamp(intensity, 1, sad.Length);
        muteAllSpecial();
        for (int i = 0; i < intensity; i++)
        {
            sad[i].mute = false;
        }
        globalMoodChange("Sad");
    }


    private void globalMoodChange(string tagToActivate)
    {
        for (int i = 0; i < playerObjects.Length; i++)
        {
            playerObjects[i].GetComponent<controll>().change(tagToActivate);
        }

        for (int i = 0; i < happyObjects.Length; i++)
        {
            happyObjects[i].GetComponent<controll>().change(tagToActivate);
        }

        for (int i = 0; i < sadObjects.Length; i++)
        {
            sadObjects[i].GetComponent<controll>().change(tagToActivate);
        }
    }


    public void playMood(int intensity)
    {
        if (intensity > 0)
        {
            playHappy(intensity);
        }
        else if (intensity < 0)
        {
            playSad((int)Mathf.Abs(intensity));
        }
        else
        {
            muteAllSpecial();
        }
    }

    public void startMoodWave()
    {
        StartCoroutine(moodWave());
    }

    private void updateMood()
    {
        if (moodCounter == moodCounterMax && moodImproving ? !(moodImproving = false) : false)
        {
            fadingIn = !fadingIn;
            Debug.Log("Habe die Fade richtung umgedreht!");
            Debug.Log(fading);
        }

        if (moodCounter == moodCounterMin && !moodImproving ? moodImproving = true : false)
        {
            fadingIn = !fadingIn;
            Debug.Log("Habe die Fade richtung umgedreht!");
            Debug.Log(fading);
        }

        if (moodCounter == 0)
        {
            fadingIn = !fadingIn;
            Debug.Log("Habe die Fade richtung umgedreht!");
            Debug.Log(fading);
        }

        if (moodImproving)
        {
            moodCounter++;
        }
        else
        {
            moodCounter--;
        }
    }

    private void fade(float progress, int moodCounter)
    {
        if (moodCounter > 0)
        {
            happy[moodCounter - 1].volume = progress * progress * globalVolume;
        }
        else if (moodCounter < 0)
        {
            sad[(-moodCounter) - 1].volume = progress * progress * globalVolume;
        }
        else
        {
            //Do Nothing.
        }
    }

    IEnumerator moodWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenMoodUpdate);
            updateMood();
            playMood(moodCounter);
            fading.Add(StartCoroutine(fader()));
            Debug.Log(moodCounter);
        }
    }

    IEnumerator fader()
    {
        int moodCounterToWorkOn = moodCounter;
        int startI;
        int endI;
        if (fadingIn)
        {
            startI = 1;
            endI = 100;
        }
        else
        {
            startI = 99;
            endI = 0;
        }

        for (int i = startI; fadingIn ? i <= endI : i >= endI;)
        {
            fade(((float)i) / 100, moodCounterToWorkOn);

            if (fadingIn)
            {
                i++;
            }
            else
            {
                i--;
            }

            yield return new WaitForSeconds(timeBetweenMoodUpdate / 100);
        }
        StopCoroutine(fading[0]);
        fading.RemoveAt(0);
    }
}
