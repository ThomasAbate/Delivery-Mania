using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance;

    [SerializeField] private GameObject gameOverUI;
    [HideInInspector] public bool isGameOver;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
        gameOverUI.SetActive(false);
        isGameOver = false;
    }

    public void ActivateGameOverUI()
    {
        gameOverUI.SetActive(true);
        LookWithMouse.Instance.UnlockMouse();
    }

    public void Retry()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
