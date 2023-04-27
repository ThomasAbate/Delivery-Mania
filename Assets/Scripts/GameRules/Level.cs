using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "New Level")]
public class Level : ScriptableObject
{
    public int levelNumber = 0;
    public int time = 120;
    public int yellowAmount = 10;
    public int greenAmount = 10;
    public int redAmount = 10;
}
