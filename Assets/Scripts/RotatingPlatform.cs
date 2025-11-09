using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotatingSpeed = 50f;
    public float timeDilation = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeDilation == 0.0f)
            return;
        transform.Rotate(Vector3.forward, timeDilation * rotatingSpeed * Time.deltaTime);
    }
}
