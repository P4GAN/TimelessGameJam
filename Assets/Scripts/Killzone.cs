using UnityEngine;

public class Killzone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Killzone triggered");
        Debug.Log(other.gameObject.tag);
        if (other.CompareTag("Player"))
        {
            GameManager.instance.Restart();
        }
    }
}
