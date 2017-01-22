using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class controll : MonoBehaviour
{
    public enum objectType { Nothing, Death, Plattform }

    public objectType type = objectType.Nothing;
    private string activeString;


    void Update()
    {
        switch (type)
        {
            case objectType.Plattform: break;
            default: break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (activeString == gameObject.tag)
        {
            Debug.Log("Tod");
            if (other.tag == "Player")
            {
                switch (type)
                {
                    case objectType.Death: death(); break;
                    default: break;
                }
            }
        }
    }

    private void death()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void change(string tagString)
    {
        activeString = tagString;
        changePlayer(tagString);

        updateGraphic();
    }

    private void changePlayer(string mood)
    {
        
        if (gameObject.tag == "Player")
        {
            player playerData = gameObject.GetComponent<player>();
            switch (mood)
            {
                case "Happy":
                    playerData.jumpHightTweaker = 10;
                    playerData.wallkingTweaker = 0.6f;
                    playerData.flyTweaker = 0.2f;
                    playerData.maxSpeed = 10;
                    break;
                case "Sad":
                    playerData.jumpHightTweaker = 6.5f;
                    playerData.wallkingTweaker = 0.3f;
                    playerData.flyTweaker = 0.4f;
                    playerData.maxSpeed = 10;
                    break;
                default: break;
            }
        }
    }

    private void updateGraphic()
    {
        if (gameObject.tag == activeString)
        {
            gameObject.SetActive(true);
        }

        else
        {
            gameObject.SetActive(false);
        }
    }
}
