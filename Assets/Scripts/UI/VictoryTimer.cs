using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryTimer : MonoBehaviour
{
    public static VictoryTimer Instance;

    private float timeRemaining;
    [HideInInspector] public bool startGame;
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
        Time.timeScale = 1;
        startGame = false;
        timeRemaining = LevelManager.Instance.level.time;
    }

    private void Start()
    {
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
                    Time.timeScale = 0;
                    GameOver.Instance.ActivateGameOverUI();
                    GameOver.Instance.isGameOver = true;
                }
            }
        }
        
    }

    public void StartTimer()
    {
        startGame = true;
        timerText.enabled = true;
    }
}
