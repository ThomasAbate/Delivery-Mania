using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Level level;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
        if (!level)
        {
            Debug.LogWarning("Missing level");
            level = new Level();
        }
    }

    public void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene+1);
        Debug.LogWarning("Last Level");
    }
}