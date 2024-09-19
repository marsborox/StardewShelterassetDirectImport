using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class UnitStatsAndInfo : MonoBehaviour
{
    public string taskString;
    public string combatActivityString;


    public int range;
    public int damage;
    public float attackSpeed;
    public int health;

    public UnitSO unitSettings;
    
    void Start()
    {
        
    }


    public void SetStats()
    {
        range = unitSettings.range;
        damage = unitSettings.damage;
        attackSpeed = unitSettings.attackSpeed;
        health = unitSettings.health;
    }
}
