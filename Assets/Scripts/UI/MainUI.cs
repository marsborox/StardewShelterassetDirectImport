using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MainUI : MonoBehaviour
{
    [Header("BasicInfo")]
    public TextMeshProUGUI health;
    public TextMeshProUGUI mana;
    public TextMeshProUGUI energy;
    [Header("Stats")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI xp;
    public TextMeshProUGUI activity;
    public TextMeshProUGUI combatState;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI range;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI attackSpeed;

    public GameObject activeUnit;

    public int temp;

    UnitHealth unitHealth;
    UnitStatsAndInfo unitStatsAndInfo;
    public void ActiveUnit()
    {
        unitHealth = activeUnit.GetComponent<UnitHealth>();




        unitStatsAndInfo=activeUnit.GetComponent<UnitStatsAndInfo>();
    }
    public void RefreshUI()
    { 
        
        //mana
        //energy
        //name
        //xp
        //activity.SetText(activeUnit.);
    }
    void Update()
    {
        if (activeUnit != null)
        {
            health.SetText(activeUnit.GetComponent<UnitHealth>().healthCurrent + " / " + activeUnit.GetComponent<UnitHealth>().healthMax.ToString());



            range.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().range.ToString());
            damage.SetText(activeUnit.GetComponent <UnitStatsAndInfo>().damage.ToString());
            attackSpeed.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().attackSpeed.ToString());
        }

        {
            //health.SetText(unitHealth.healthCurrent + " / " + unitHealth.healthCurrent);




            //shits broken for some reason
            //or where is the reason we should expect this crap to work??
            //range.SetText(unitStatsAndInfo.range.ToString());

            //damage.SetText(unitStatsAndInfo.damage.ToString());
            //attackSpeed.SetText(unitStatsAndInfo.attackSpeed.ToString());
        }
    }
}
