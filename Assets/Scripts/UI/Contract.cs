using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Contract : MonoBehaviour
{
    [SerializeField] private TextMeshPro redText;
    [SerializeField] private TextMeshPro yellowText;
    [SerializeField] private TextMeshPro greenText;
    void Start()
    {
        redText.text = "Red Box : " + LevelManager.Instance.level.redAmount;
        yellowText.text = "Yellow Box : " + LevelManager.Instance.level.yellowAmount;
        greenText.text = "Green Box : " + LevelManager.Instance.level.greenAmount;
    }
}
