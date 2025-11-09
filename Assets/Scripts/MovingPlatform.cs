using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    public float speed = 2f;
    public float timeDilation = 1.0f;

    private Vector3 targetPosition;
    private void Awake()
    {
        targetPosition = pointB.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timeDilation == 0.0f)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, timeDilation * speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (targetPosition == pointB.position)
            {
                targetPosition = pointA.position;
            }
            else
            {
                targetPosition = pointB.position;
            }
        }
    }
}
