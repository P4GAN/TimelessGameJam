using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    public Animator playeranim;
    public GameObject myGameObject;
    public AudioClip winSound;
    public AudioSource soundManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("LevelExit");

        }
    }

    IEnumerator LevelExit()
    {
        //anim.SetTrigger("Exit");
        Destroy(myGameObject.GetComponent("PlayerController"));
        yield return new WaitForSeconds(0.1f);
        playeranim.SetTrigger("win");
        soundManager.PlayOneShot(winSound);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // Do something after flag anim

    }
}
