using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Finisher : MonoBehaviour
{
    Coroutine winningTimer = null;
    public void OnTriggerEnter(Collider other)
    {
        if (winningTimer == null)
            StartCoroutine(winning());
    }

    public void OnTriggerStay(Collider other)
    {
        if (winningTimer == null)
            StartCoroutine(winning());
    }

    private void winMessage()
    {
        Debug.Log("Gewonnen");
    }

    IEnumerator winning()
    {
        winMessage();
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainLevel");
    }
}
