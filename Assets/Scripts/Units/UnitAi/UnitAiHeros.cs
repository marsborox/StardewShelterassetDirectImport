using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Task { IDLE, LOCATIONACTIVITY, COMBAT, ARENARESOURCE, MOVEMENT, RANDOMAUTOMOVE, TRAVELING, ADVENTURING, OTHER }//only heros will need this
public class UnitAiHeros : UnitAiBase
{
    public Task task;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TaskSwitch();
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

        }
    }


}
