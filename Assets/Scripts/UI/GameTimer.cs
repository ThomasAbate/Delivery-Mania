using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    private float timeRemaining;
    [HideInInspector] public bool startGame;
    public TextMeshProUGUI timerText;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
        Time.timeScale = 1;
        startGame = false;
    }

    private void Start()
    {
        timeRemaining = LevelManager.Instance.level.time;
        timerText.enabled = false;
    }

    private void Update()
    {
        if (startGame)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timerText.text = string.Format("{0:00} : {1:00}", (int)timeRemaining / 60, (int)timeRemaining % 60);
            }
            else
            {
                if (!GameOver.Instance.isGameOver)
                {
                    GameOver.Instance.ActivateGameOverUI();
                    GameOver.Instance.isGameOver = true;
                }
            }
        }
    }

    public void StartTimer()
    {
        if (LevelManager.Instance.level.time == 0) return;
        else
        {
            timerText.enabled = true;
            startGame = true;
        }
    }

    public void CancelTimer()
    {
        startGame = false;
        timerText.text = "";
    }
}
