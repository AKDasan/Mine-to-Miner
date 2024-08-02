using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MineIngot", menuName = "MineIngot")]
public class MineIngotSO : ScriptableObject
{
    public MineType mineType;
    public GameObject resourceIngot;
}

