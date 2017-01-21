using UnityEngine;
using System.Collections;

public class player : MonoBehaviour
{
    private Rigidbody rb;
    private bool jumping = false;
    private bool landing = false;
    private Animator playerAnimation;

    public float jumpHightTweaker = 10;
    public float wallkingTweaker = 1;
    public float flyTweaker = 0.5f;
    public float maxSpeed = 5;

    public isGrounded grounder;
    public isSided[] sides;

    private Coroutine timeout;
    private bool skippable = true;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerAnimation = GetComponentInChildren<Animator>();

    }

    void Update()
    {
        if (jumping && !grounder._grounded)
        {
            landing = true;
        }

        if (landing && grounder._grounded)
        {
            landing = false;
            jumping = false;
            playerAnimation.SetTrigger("IsGroundend");
        }

        if (Input.GetAxis("Fire1") == 1 && skippable)
        {
            skippable = false;
            timeout = StartCoroutine(timer());
            music._Reference.next();
        }

        if (Input.GetAxisRaw("Jump") == 1 && grounder._grounded)
        {
            jump();
        }
        if (rb.velocity.x > 0.01)
        {

            transform.Find("Playermodell").LookAt(new Vector3(1000, 0, 0));
            playerAnimation.SetBool("IsWalking", true);
        }
        else if (rb.velocity.x < -0.01)
        {
            playerAnimation.SetTrigger("IsWalking");
            transform.Find("Playermodell").LookAt(new Vector3(-1000, 0, 0));
            playerAnimation.SetBool("IsWalking", true);
        }
        else
        {
            playerAnimation.SetBool("IsWalking", false);

        }
        horizontalMove();
    }

    private void horizontalMove()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);

        if (landing && sides[0]._touching)
        {
            input.x = Mathf.Clamp(input.x, 0, 1);
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
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y, 0);
    }

    public void jump()
    {
        if (!jumping)
        {
            playerAnimation.SetTrigger("Jumping");
            rb.AddForce(new Vector3(0, jumpHightTweaker, 0), ForceMode.VelocityChange);
        }
        jumping = true;
    }


    IEnumerator timer()
    {
        yield return new WaitForSeconds(0.5f);
        skippable = true;
        StopCoroutine(timeout);
    }
}
