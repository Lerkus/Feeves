using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoToLvl2 : MonoBehaviour {

    Text Loading;


    public void ButtonPress()
    {
        Loading = GetComponent<Text>();
        Loading.text = "Loading...";

        SceneManager.LoadScene("MainMenue");
    }
}
