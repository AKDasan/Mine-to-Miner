using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mine", menuName = "Mine")]
public class MineSO : ScriptableObject
{
    public MineType mineType;
    public MineValue mineValue;

    public GameObject resourcePiece;
}

public enum MineType
{
    Stone,
    Coal,
    Iron,
    Gold,
    Emerald,
    Diamond
}

public enum MineValue
{
    Stone_Coal = 1,
    Iron = 2,
    Gold = 3,
    Emerald = 4,
    Diamond = 5,
}
