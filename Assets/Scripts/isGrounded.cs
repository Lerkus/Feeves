using UnityEngine;
using System.Collections;

public class isGrounded : MonoBehaviour
{

    private bool grounded = true;
    public bool _grounded
    {
        get { return grounded; }
        private set { }
    }

    public void OnTriggerEnter(Collider other)
    {
        grounded = true;
    }

    public void OnTriggerStay(Collider other)
    {
        grounded = true;
    }

    public void OnTriggerExit(Collider other)
    {
        grounded = false;
    }
}
