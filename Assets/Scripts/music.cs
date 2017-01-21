using UnityEngine;
using System.Collections;

public class music : MonoBehaviour {

    public AudioSource[] basic;
    public AudioSource[] happy;
    public AudioSource[] sad;

    private music musicReference;

    private int moodCounter = 0;

    public music _Reference
    {
        get { return musicReference; }
        private set { }
    }

    public void Awake()
    {
        musicReference = this;
        muteAllSpecial();
        playHappy(111);
        playSad(111);
        setVolumeAll(0.25f);
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
        intensity = (int) Mathf.Clamp(intensity,0,happy.Length);
        muteAllSpecial();
        for (int i = 0; i < intensity; i++)
        {
            happy[i].mute = false;
        }
    }

    public void playSad(int intensity)
    {
        intensity = (int)Mathf.Clamp(intensity, 0, sad.Length);
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
        else
        {
            playSad( (int)Mathf.Abs(intensity));
        }
    }

    IEnumerator moodWave()
    {
        yield return new WaitForSeconds(1f);
        
    }


}
