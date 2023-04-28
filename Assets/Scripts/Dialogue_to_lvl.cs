using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue_to_lvl : MonoBehaviour
{
    public void NextScean()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}