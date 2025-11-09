using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Lore()
    {
        SceneManager.LoadScene("Lore");
    }
    public void Levels()
    {
        SceneManager.LoadScene("Level Select");
    }
    public void Level_1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Level_2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void Level_3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void Level_4()
    {
        SceneManager.LoadScene("Level 4");
    }
    public void Level_5()
    {
        SceneManager.LoadScene("Level 5");
    }
    public void Level_6()
    {
        SceneManager.LoadScene("Level 6");
    }
    public void Level_7()
    {
        SceneManager.LoadScene("Level 7");
    }
    public void Level_8()
    {
        SceneManager.LoadScene("Level 8");
    }
    public void Level_9()
    {
        SceneManager.LoadScene("Level 9");
    }
    public void Level_10()
    {
        SceneManager.LoadScene("Last Level");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
