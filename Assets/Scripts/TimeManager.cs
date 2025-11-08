using UnityEngine;
using System.Collections.Generic;

public class TimeManager : MonoBehaviour
{    
    public GameObject PhysicsObjectsParent;

    List<GameObject> physicsObjects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform child in PhysicsObjectsParent.transform)
        {
            physicsObjects.Add(child.gameObject);
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
        for (int i = 0; i < physicsObjects.Count; i++)
        {
            Rigidbody2D rb = physicsObjects[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Static;
            }
        }
    }

    public void StartTime() 
    {
        for (int i = 0; i < physicsObjects.Count; i++)
        {
            Rigidbody2D rb = physicsObjects[i].GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
