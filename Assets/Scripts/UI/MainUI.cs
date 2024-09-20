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
    public TextMeshProUGUI task;
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


            //task.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().taskString);

            combatState.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().combatActivityString);

            range.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().range.ToString());
            damage.SetText(activeUnit.GetComponent <UnitStatsAndInfo>().damage.ToString());
            attackSpeed.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().attackSpeed.ToString());
            //attackSpeed.SetText(activeUnit.GetComponent<UnitAiBase>().attackSpeed.ToString());
            task.SetText(activeUnit.GetComponent<UnitAiBase>().GetActivity());// like newest kind of stuff
            /*
            if (x is UnitAiHeros)
            {
                task.SetText(Enum.GetName(typeof(Task), ((UnitAiHeros)x).task));

            }
            else if (x is UnitAiMobs)
            {
                task.SetText("doing MOB stuff");
            }
            *****************************************
            
            switch(x)
            {
                case UnitAiHeros y:
                    task.SetText(Enum.GetName(typeof(Task), y.task));
                    break;
                case UnitAiMobs y:
                    task.SetText(Enum.GetName(typeof(MobsActivity), y.mobsActivity));
                    break;
            }
            */

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
