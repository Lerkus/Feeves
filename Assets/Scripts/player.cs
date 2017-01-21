using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
    private Rigidbody rb;
    private bool jumping = false;
    public float jumpHightTweaker = 10;
    public float wallkingTweaker = 1;
    public float flyTweaker = 0.5f;
    private bool landing = false;

    public isGrounded grounder;
    public isSided[] sides;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(jumping && !grounder._grounded)
        {
            landing = true;
        }

        if (landing && grounder._grounded)
        {
            landing = false;
            jumping = false;
        }

        if (Input.GetAxisRaw("Jump") == 1 && grounder._grounded)
        {
            jump();
        }

        horizontalMove();
    }

    private void horizontalMove()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (landing && sides[0]._touching)
        {
            input.x = Mathf.Clamp(input.x,0,1);
        }
        else if (landing && sides[1]._touching)
        {
            input.x = Mathf.Clamp(input.x, -1, 0);
        }
        else if (!grounder._grounded)
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
        if (!jumping)
        {
            rb.AddForce(new Vector3(0, jumpHightTweaker, 0), ForceMode.VelocityChange);
        }
        jumping = true;
    }
}
