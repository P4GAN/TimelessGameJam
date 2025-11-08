using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    public GameObject gravityObjectsParent;
    public GameObject movingPlatformsParent;

    List<GameObject> gravityObjects = new List<GameObject>();
    List<GameObject> movingPlatforms = new List<GameObject>();

    public bool timeAlreadyStopped = false;
    public List<Vector2> storedVelocities = new List<Vector2>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in gravityObjectsParent.transform)
        {
            gravityObjects.Add(child.gameObject);
            storedVelocities.Add(Vector2.zero);
        }
        foreach (Transform child in movingPlatformsParent.transform)
        {
            movingPlatforms.Add(child.gameObject);
        }
        StopTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StopTime();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartTime();
        }
    }

    public void StopTime()
    {
        for (int i = 0; i < gravityObjects.Count; i++)
        {
            Rigidbody2D rb = gravityObjects[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 0;
                if (!timeAlreadyStopped)
                { // at the moment time starts, preserve current momenta
                    storedVelocities[i] = rb.linearVelocity;
                    timeAlreadyStopped = true;
                }
                rb.linearVelocity = Vector2.zero;
            }
        }
        for (int i = 0; i < movingPlatforms.Count; i++)
        {
            MovingPlatform mp = movingPlatforms[i].GetComponent<MovingPlatform>();
            if (mp != null)
            {
                mp.isMoving = false;
            }
        }
    }

    public void StartTime()
    {
        for (int i = 0; i < gravityObjects.Count; i++)
        {
            Rigidbody2D rb = gravityObjects[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = 1;
                if (timeAlreadyStopped)
                { // at the moment time restarts, restore previous momenta
                    rb.linearVelocity = storedVelocities[i];
                    timeAlreadyStopped = false;
                }
            }
        }
        for (int i = 0; i < movingPlatforms.Count; i++)
        {
            MovingPlatform mp = movingPlatforms[i].GetComponent<MovingPlatform>();
            if (mp != null)
            {
                mp.isMoving = true;
            }
        }
    }
}
