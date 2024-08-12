using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStatsAndInfo : MonoBehaviour
{

    int range;
    int damage;
    float attackSpeed;
    public int health;

    public UnitSO unitSettings;
    // Start is called before the first frame update
    void Start()
    {
        //damage = unitSettings.damage;
        attackSpeed = unitSettings.attackSpeed;
        health = unitSettings.health; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
