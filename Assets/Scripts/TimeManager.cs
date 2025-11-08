using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    public GameObject gravityObjectsParent;
    public GameObject movingPlatformsParent;

    List<GameObject> gravityObjects = new List<GameObject>();
    List<GameObject> movingPlatforms = new List<GameObject>();

    int timeDilationStage = 0;
    List<float> timeDilations = new List<float> { 1.0f, 0.95f, 0.9f, 0.85f, 0.8f, 0.75f, 0.7f, 0.65f, 0.6f, 0.5f, 0.4f, 0.35f, 0.3f, 0.25f, 0.1f, 0.05f, 0.0f };

    bool timeAlreadyStopped = true;
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
        timeAlreadyStopped = true;
        for (int i = 0; i < movingPlatforms.Count; i++)
        {
            MovingPlatform mp = movingPlatforms[i].GetComponent<MovingPlatform>();
            if (mp != null)
            {
                mp.isMoving = false;
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
        timeAlreadyStopped = false;
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
