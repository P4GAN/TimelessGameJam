using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{    
    public GameObject gravityObjectsParent;
    public GameObject movingPlatformsParent;

    List<GameObject> gravityObjects = new List<GameObject>();
    List<GameObject> movingPlatforms = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in gravityObjectsParent.transform)
        {
            gravityObjects.Add(child.gameObject);
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
            RotatingPlatform rp = movingPlatforms[i].GetComponent<RotatingPlatform>();
            if (rp != null)
            {
                rp.isMoving = false;
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
            }
        }
        for (int i = 0; i < movingPlatforms.Count; i++)
        {
            MovingPlatform mp = movingPlatforms[i].GetComponent<MovingPlatform>();
            if (mp != null)
            {
                mp.isMoving = true;
            }
            RotatingPlatform rp = movingPlatforms[i].GetComponent<RotatingPlatform>();
            if (rp != null)
            {
                rp.isMoving = true;
            }
        }
    }
}
