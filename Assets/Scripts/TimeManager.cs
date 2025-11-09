using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{
    public GameObject gravityObjectsParent;
    public GameObject movingPlatformsParent;

    List<GameObject> gravityObjects = new List<GameObject>();
    List<GameObject> movingPlatforms = new List<GameObject>();

    public int timeDilationStage = 0;
    List<float> timeDilations = new List<float> { 1.000f, 0.855f, 0.720f, 0.595f, 0.480f, 0.375f, 0.280f, 0.195f, 0.120f, 0.055f, 0.000f }; // quadratic-ish
    // List<float> timeDilations = new List<float> { 1.000f, 0.600f, 0.359f, 0.215f, 0.129f, 0.077f, 0.045f, 0.025f, 0.013f, 0.006f, 0.000f }; // exponential-ish

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

    private void GravityObjectApplyDilation(bool timeJustStopped)
    {
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

    private void MovingPlatformUpdateDilation()
    {
        for (int i = 0; i < movingPlatforms.Count; i++)
        {
            MovingPlatform mp = movingPlatforms[i].GetComponent<MovingPlatform>();
            float dilation = timeDilations[timeDilationStage];
            if (mp != null)
            {
                mp.timeDilation = dilation;
            }
            RotatingPlatform rp = movingPlatforms[i].GetComponent<RotatingPlatform>();
            if (rp != null)
            {
                rp.timeDilation = dilation;
            }
        }
    }

    public void StopTime()
    {
        bool timeJustStopped = timeDilationStage == 0;
        if (timeDilationStage < timeDilations.Count - 1)
            ++timeDilationStage;

        GravityObjectApplyDilation(timeJustStopped);
        MovingPlatformUpdateDilation();
    }

    private void GravityObjectRemoveDilation()
    {
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
        if (timeDilationStage == 0)
            return;
        --timeDilationStage;

        GravityObjectRemoveDilation();
        MovingPlatformUpdateDilation();
    }
}
