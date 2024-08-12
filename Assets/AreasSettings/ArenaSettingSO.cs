using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ArenaSetting", fileName = "New Arena")]

public class ArenaSettingSO : ScriptableObject
{
    [SerializeField] public int maxObjectsInArea;
    [SerializeField] public int maxLootChestsInArea;
    [SerializeField] public int maxEnemyUnitsInArea;
}
