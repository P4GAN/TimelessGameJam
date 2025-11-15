using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowFallPlatform : MonoBehaviour
{
    private Rigidbody2D rb;
    public float strength;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector2.up * rb.gravityScale * strength, ForceMode2D.Force);
    }
    }
