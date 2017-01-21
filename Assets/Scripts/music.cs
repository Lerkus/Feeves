using UnityEngine;
using System.Collections;

public class music : MonoBehaviour {

    public AudioSource[] basic;
    public AudioSource[] happy;
    public AudioSource[] sad;

    private music musicReferenz;

    void Awake()
    {
        musicReferenz = this;
        muteAllSpecial();
    }

    void muteAllSpecial()
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

    void playHappy(int intensity)
    {
        intensity = (int) Mathf.Clamp(intensity,0,happy.Length);
        muteAllSpecial();
        for (int i = 0; i < intensity; i++)
        {
            happy[i].mute = false;
        }
    }
    void playSad(int intensity)
    {
        intensity = (int)Mathf.Clamp(intensity, 0, sad.Length);
        muteAllSpecial();
        for (int i = 0; i < intensity; i++)
        {
            sad[i].mute = false;
        }
    }
}
