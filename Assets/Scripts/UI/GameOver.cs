using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        EventSystem.current.SetSelectedGameObject(gameOverUI.transform.GetChild(0).gameObject);
        if (LookWithMouse.Instance.playerInput.currentControlScheme == "KeyboardMouse")
        {
            LookWithMouse.Instance.isUiActive = true;
            LookWithMouse.Instance.UnlockMouse();
        }
        LookWithMouse.Instance.gamepadSensitivity = 0;
        LookWithMouse.Instance.mouseSensitivity = 0;
    }

    public void Retry()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
