using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//only heros will need this
public enum Task { IDLE, LOCATIONACTIVITY, COMBAT, ARENARESOURCE, MOVEMENT, RANDOMAUTOMOVE, TRAVELING, ADVENTURING, OTHER }
public class UnitAiHeros : UnitAiBase
{
    public Task task;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(task.ToString());
        unitStatsAndInfo.taskString = task.ToString();
        TaskSwitch();
    }
    public override string GetActivity()
    {
        return Enum.GetName(typeof(Task), task);
    }
    void TaskSwitch()
    {
        switch (task)
        {

            case Task.ADVENTURING:
                {
                    if (unitCombat.combatActivity == CombatActivity.RESTING)
                    {
                        unitHealth.Resting2();
                    }
                    else if ((unitHealth.healthState == HealthState.LOW) && !unitCombat.inCombat)
                    {
                        unitHealth.Resting2();
                        //unitCombat.combatActivity = CombatActivity.RESTING;
                    }
                    else
                    {
                        unitCombat.KillingEnemies();
                    }
                    break;
                    /*
                    if ( (unitHealth.healthLow | unitHealth.isResting)&&!inCombat)
                    {
                        unitHealth.Resting2();
                    }
                    else
                    {
                        KillingMobs();
                    }
                    break;*/
                }

            case Task.OTHER:
                {
                    break;
                }
            case Task.COMBAT:
                {

                    break;
                }
            case Task.IDLE:
                {
                    break;
                }


        }
    }


}
