using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryTimer : MonoBehaviour
{
    public static VictoryTimer Instance;

    [SerializeField] private float timeRemaining;
    [HideInInspector] public bool startGame;

    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
        startGame = false;
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
                Time.timeScale = 0;
            }
        }
        
    }

    public void StartTimer()
    {
        startGame = true;
        timerText.enabled = true;
    }
}
