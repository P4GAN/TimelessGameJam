
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;

    public float jumpForce = 60f;
    public float jumpCancelStrength = 100f;
    public float floatSlow = 90f;
    //public float doubleJumpForce = 40f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Rigidbody2D rb;
    private bool isGroundedBool = false;
    //private bool canDoubleJump = true;
    private bool landing = true;
    private bool falling = true;
    private bool Jumped = false;
    private bool jumpCancelBool = false;
    private float mayJump = 0.2f;

    public Animator playeranim;
   
    private float moveX;
    public bool isPaused = false;

    public ParticleSystem footsteps;
    private ParticleSystem.EmissionModule footEmissions;

    public ParticleSystem ImpactEffect;
    private bool wasonGround;

    public TimeManager TimeManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        footEmissions = footsteps.emission;
        
    }

    private void OnDestroy () 
    {
    rb.linearVelocity = new Vector2(0, 0); // Zero out vertical 
    }
    private void Update()
    {
        mayJump -= Time.deltaTime;
        isGroundedBool = IsGrounded();
        if (!isGroundedBool)
        {
            landing = true;
            TimeManager.StartTime();
        }
        else if (isGroundedBool)
        {
            TimeManager.StopTime();
        }


        if (isGroundedBool)
        {
            if (landing)
            {
                landing = false;
                playeranim.SetTrigger("idle");
            }
            moveX = Input.GetAxis("Horizontal");
            mayJump = 0.2f;
            Jumped = false;
            falling = true;
            //canDoubleJump = true;
        }

        SetAnimations();

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Vertical")) // Player starts pressing the button
        {
            if (mayJump > 0 && Jumped == false)
            {
                Jump(jumpForce);
                mayJump = 0;
                Jumped = true;
                landing = true;
                falling = true;
            }
            /*else if(isGroundedBool == false && canDoubleJump == true)
            {
                Jump(doubleJumpForce);
                canDoubleJump = false;
                jumpCancelBool = false;
            }*/
        }
        if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.M))
        {

            if (isGroundedBool == false)
            {
                if (rb.linearVelocity.y <= 0f)
                {
                    rb.AddForce(Vector2.up * floatSlow, ForceMode2D.Force);
                }
                if (falling == true)
                {
                    playeranim.SetTrigger("fall");
                    falling = false;
                }
            }
        }
        else
        {
            if (isGroundedBool == false)
            {
                playeranim.SetTrigger("jump");
            }
        }

        if ((Input.GetButtonUp("Jump") || Input.GetButtonUp("Vertical")) && !isGroundedBool) // Player stops pressing the button
        {
            jumpCancelBool = true;

        }

        if (jumpCancelBool == true)
        {
            if (rb.linearVelocity.y <= 0.1f)
            {
                jumpCancelBool = false;
            }
            rb.AddForce(Vector2.down * jumpCancelStrength, ForceMode2D.Force);
        }

        if (moveX != 0)
        {
            FlipSprite(moveX);
        }

        //impactEffect

        if (!wasonGround && isGroundedBool)
        {
            ImpactEffect.gameObject.SetActive(true);
            ImpactEffect.Stop();
            ImpactEffect.transform.position = new Vector2(footsteps.transform.position.x, footsteps.transform.position.y - 0.2f);
            ImpactEffect.Play();
        }

        wasonGround = isGroundedBool;


    }
    public void SetAnimations()
    {
        if (moveX != 0 && isGroundedBool)
        {
            playeranim.SetBool("run", true);
        }
        else
        {
            playeranim.SetBool("run",false);
        }       
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
        {
            // Moving right, flip sprite to the right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction < 0)
        {
            // Moving left, flip sprite to the left
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void FixedUpdate()
    {
        // Player movement
        moveX = Input.GetAxis("Horizontal");

        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        
    }

    private void Jump(float jumpForce)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // Zero out vertical velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        playeranim.SetTrigger("jump");
    }

    private bool IsGrounded()
    {
        float rayLength = 0.25f;
        Vector2 rayOrigin = new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y - 0.01f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }
}