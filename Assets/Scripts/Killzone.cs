using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioSource soundManager;

    public GameObject myGameObject;
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
        if (other.CompareTag("Player"))
        {
            StartCoroutine("KillPlayer");
        }
    }

    IEnumerator KillPlayer()
    {
        //anim.SetTrigger("Exit");
        Destroy(myGameObject.GetComponent("PlayerController"));
        soundManager.PlayOneShot(deathSound);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.Restart();
    }
}
