using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WinSystem : MonoBehaviour
{
    public static WinSystem Instance;

    [SerializeField] private GameObject victoryUI;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private string tutorialVictoryText;
    [SerializeField] private string victoryText;
    [HideInInspector] public bool isGameWin;
    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
        victoryUI.SetActive(false);
        isGameWin = false;
    }

    public void ShowVictoryUI()
    {
        victoryUI.SetActive(true);
        SetVictoryText();
        GameTimer.Instance.CancelTimer();
        EventSystem.current.SetSelectedGameObject(victoryUI.transform.GetChild(0).gameObject);
        if (LookWithMouse.Instance.playerInput.currentControlScheme == "KeyboardMouse")
        {
            LookWithMouse.Instance.UnlockMouse();
        }
        LookWithMouse.Instance.gamepadSensitivity = 0;
        LookWithMouse.Instance.mouseSensitivity = 0;
        PlayerController.Instance.lockMovements = true;
        PlayerInteraction.Instance.lockInteractions = true;
        isGameWin = true;
    }

    private void SetVictoryText()
    {
        if(LevelManager.Instance.level.time == 0)
        {
            textMeshPro.text = tutorialVictoryText;
        }
        else
        {
            textMeshPro.text = victoryText + GameTimer.Instance.timerText.text;
        }
    }
}
