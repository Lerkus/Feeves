using UnityEngine;
using System.Collections;

public class isSided : MonoBehaviour {

    private bool sided = false;
    public bool _touching
    {
        get { return sided; }
        private set { }
    }

    public void OnTriggerEnter(Collider other)
    {
        sided = true;
    }

    public void OnTriggerStay(Collider other)
    {
        sided = true;
    }

    public void OnTriggerExit(Collider other)
    {
        sided = false;
    }
}
