using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTrigger : MonoBehaviour
{
    //public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("LevelExit");
            Debug.Log("Yes");
        }
    }

    IEnumerator LevelExit()
    {
        //anim.SetTrigger("Exit");
        yield return new WaitForSeconds(0.1f);
        
        Debug.Log("Yes");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);
        // Do something after flag anim

    }
}
