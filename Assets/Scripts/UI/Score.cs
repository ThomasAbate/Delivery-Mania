using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Score : MonoBehaviour
{
    public static Score Instance;

    [SerializeField] private TextMeshProUGUI redCounter;
    [SerializeField] private TextMeshProUGUI yellowCounter;
    [SerializeField] private TextMeshProUGUI greenCounter;

    [HideInInspector] public int redCount = 0;
    [HideInInspector] public int yellowCount = 0;
    [HideInInspector] public int greenCount = 0;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        redCounter.text = redCount.ToString();
        yellowCounter.text = yellowCount.ToString();
        greenCounter.text = greenCount.ToString();
    }

    public void SetCounter(BoxColor color, int number)
    {
        switch (color)
        {
            case BoxColor.Red:
                redCount = number;
                SetCounterText(BoxColor.Red);
                break;
            case BoxColor.Yellow:
                yellowCount = number;
                SetCounterText(BoxColor.Yellow);
                break;
            case BoxColor.Green:
                greenCount = number;
                SetCounterText(BoxColor.Green);
                break;
        }
    }


    public void AddToCounter(BoxColor color, int number)
    {
        switch (color)
        {
            case BoxColor.Red:
                redCount += number;
                SetCounterText(BoxColor.Red);
                break;
            case BoxColor.Yellow:
                yellowCount += number;
                SetCounterText(BoxColor.Yellow);
                break;
            case BoxColor.Green:
                greenCount += number;
                SetCounterText(BoxColor.Green);
                break;
        }
        CheckWin();
    }

    public void RemoveFromCounter(BoxColor color, int number)
    {
        switch (color)
        {
            case BoxColor.Red:
                redCount -= number;
                SetCounterText(BoxColor.Red);
                break;
            case BoxColor.Yellow:
                yellowCount -= number;
                SetCounterText(BoxColor.Yellow);
                break;
            case BoxColor.Green:
                greenCount -= number;
                SetCounterText(BoxColor.Green);
                break;
        }
    }

    private void SetCounterText(BoxColor color)
    {
        switch (color)
        {
            case BoxColor.Red:
                redCounter.text = redCount.ToString();
                break;
            case BoxColor.Yellow:
                yellowCounter.text = yellowCount.ToString();
                break;
            case BoxColor.Green:
                greenCounter.text = greenCount.ToString();
                break;
        }
    }

    private void CheckWin()
    {
        if(redCount >= LevelManager.Instance.level.redAmount &&
           yellowCount >= LevelManager.Instance.level.yellowAmount &&
           greenCount >= LevelManager.Instance.level.greenAmount)
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }
}