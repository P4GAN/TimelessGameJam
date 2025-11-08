using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{    
    public GameObject gravityObjectsParent;

    List<GameObject> gravityObjects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in gravityObjectsParent.transform)
        {
            gravityObjects.Add(child.gameObject);
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
    }
}
