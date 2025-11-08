
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float doubleJumpForce = 8f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Rigidbody2D rb;
    private bool isGroundedBool = false;
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

    private void Update()
    {
        mayJump -= Time.deltaTime;
        isGroundedBool = IsGrounded();
        if (!isGroundedBool)
        {
            TimeManager.StartTime();
        }
        else if (isGroundedBool)
        {
            TimeManager.StopTime();
        }

        if (isGroundedBool)
        {
            moveX = Input.GetAxis("Horizontal");
            mayJump = 0.2f;

        }

        if (Input.GetButtonDown("Jump"))
        {
            if (mayJump > 0)
            {
                Jump(jumpForce);
                mayJump = 0f;
            }
        }

        SetAnimations();

        if (moveX != 0)
        {
            FlipSprite(moveX);
        }

        //impactEffect

        if(!wasonGround && isGroundedBool)
        {
            ImpactEffect.gameObject.SetActive(true);
            ImpactEffect.Stop();
            ImpactEffect.transform.position = new Vector2(footsteps.transform.position.x,footsteps.transform.position.y-0.2f);
            ImpactEffect.Play();
        }

        wasonGround = isGroundedBool;

        
    }
    public void SetAnimations()
    {
        if (moveX != 0 && isGroundedBool)
        {
            playeranim.SetBool("run", true);
            footEmissions.rateOverTime= 35f;
        }
        else
        {
            playeranim.SetBool("run",false);
            footEmissions.rateOverTime = 0f;
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
        Vector2 rayOrigin = new Vector2(groundCheck.transform.position.x, groundCheck.transform.position.y - 0.1f);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayLength, groundLayer);
        return hit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "killzone")
        {
            GameManager.instance.Death();
        }
    }
}