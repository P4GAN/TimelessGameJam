using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    public GameObject gravityObjectsParent;
    public GameObject movingPlatformsParent;

    List<GameObject> gravityObjects = new List<GameObject>();
    List<GameObject> movingPlatforms = new List<GameObject>();

    int timeDilationStage = 0;
    // List<float> timeDilations = new List<float> { 1.000f, 0.855f, 0.720f, 0.595f, 0.480f, 0.375f, 0.280f, 0.195f, 0.120f, 0.055f, 0.000f }; // quadratic-ish
    List<float> timeDilations = new List<float> { 1.00f, 0.60f, 0.36f, 0.22f, 0.13f, 0.08f, 0.05f, 0.026f, 0.009f, 0.00f }; // exponential-ish

    List<Vector2> storedVelocities = new List<Vector2>();

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

    private void StopTimeDilated()
    {
        bool timeJustStopped = timeDilationStage == 0;
        if (timeDilationStage < timeDilations.Count - 1)
            ++timeDilationStage;

        for (int i = 0; i < gravityObjects.Count; ++i)
        {
            Rigidbody2D rb = gravityObjects[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (timeJustStopped) // at the moment time stops, preserve current momenta
                    storedVelocities[i] = rb.linearVelocity;

                float dilation = timeDilations[timeDilationStage];
                rb.gravityScale = dilation;
                rb.linearVelocity = dilation * storedVelocities[i];
            }
        }
    }

    public void StopTime()
    {
        StopTimeDilated();
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

    private void StartTimeDilated()
    {
        if (timeDilationStage == 0)
            return;
        --timeDilationStage;
        for (int i = 0; i < gravityObjects.Count; ++i)
        {
            Rigidbody2D rb = gravityObjects[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float dilation = timeDilations[timeDilationStage];

                rb.gravityScale = dilation;
                rb.linearVelocity = dilation * storedVelocities[i];
            }
        }
    }

    public void StartTime()
    {
        StartTimeDilated();
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
