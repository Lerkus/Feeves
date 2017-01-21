using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
    private Rigidbody rb;
    private bool jumping = false;
    public float jumpHightTweaker = 10;
    public float wallkingTweaker = 1;
    public float flyTweaker = 0.5f;

    public isGrounded grounder;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {


        if (Input.GetAxisRaw("Jump") == 1 && grounder._grounded)
        {
            jump();
        }

        horizontalMove();
    }

    private void horizontalMove()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (!grounder._grounded)
        {
            input *= flyTweaker;
        }
        else
        {
            input *= wallkingTweaker;
        }

        rb.AddForce(input, ForceMode.VelocityChange);
    }

    public void jump()
    {
        rb.AddForce(new Vector3(0, jumpHightTweaker, 0), ForceMode.VelocityChange);
    }
}
