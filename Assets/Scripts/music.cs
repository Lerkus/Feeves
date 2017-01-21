using UnityEngine;
using System.Collections;

public class music : MonoBehaviour
{

    public AudioSource[] basic;
    public AudioSource[] happy;
    public AudioSource[] sad;

    private music musicReference;

    private int moodCounter = 0;
    private bool moodImproving = true;

    private bool moodCanBeHappy = false;
    private bool moodCanBeSad = false;

    private float globalVolume = 0.25f;

    Coroutine fading;

    public music _Reference
    {
        get { return musicReference; }
        private set { }
    }

    public void Awake()
    {
        musicReference = this;
        muteAllSpecial();
        //startMoodWave();
        setVolumeAll(globalVolume);
    }

    public void setVolumeAll(float intensity)
    {
        intensity = Mathf.Clamp01(intensity);

        for (int i = 0; i < basic.Length; i++)
        {
            basic[i].volume = intensity;
        }

        for (int i = 0; i < happy.Length; i++)
        {
            happy[i].volume = intensity;
        }

        for (int i = 0; i < sad.Length; i++)
        {
            sad[i].volume = intensity;
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
    }

    public void playSad(int intensity)
    {
        intensity = (int)Mathf.Clamp(intensity, 1, sad.Length);
        muteAllSpecial();
        for (int i = 0; i < intensity; i++)
        {
            sad[i].mute = false;
        }
    }

    public void playMood(int intensity)
    {
        if (intensity > 0)
        {
            playHappy(intensity);
        }
        else if(intensity < 0)
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
        if (moodCounter == happy.Length + 1 ? moodImproving = false : false) ;
        if (moodCounter == -sad.Length + 1 ? moodImproving = true : false) ;

        if (moodImproving)
        {
            moodCounter++;
        }
        else
        {
            moodCounter--;
        }
    }

    private void fade(float progress)
    {
        int index;

    }

    IEnumerator moodWave()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            updateMood();
            playMood(moodCounter);
            Debug.Log(moodCounter);
        }
    }

    IEnumerator fader()
    {
        for(int i = 1; i < 10; i++)
        {
            fade(((float)i)/10);
            yield return new WaitForSeconds(1);
        }
        StopCoroutine(fading);
    }
}
