using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.LogWarning("Not Codded Yet");
    }
}
