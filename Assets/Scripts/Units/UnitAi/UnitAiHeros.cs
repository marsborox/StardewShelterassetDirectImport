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
                    if (activity == Activity.RESTING)
                    {
                        unitHealth.Resting2();
                    }
                    else if ((unitHealth.healthState == HealthState.LOW) && !inCombat)
                    {
                        //unitHealth.Resting();
                        activity = Activity.RESTING;
                    }
                    else
                    {
                        KillingEnemies();
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

    void KillingEnemies()
    {
        //Debug.Log("arenacombat state");
        if (target == null)
        {
            unitTargetPicker.FindClosestEnemy();
            //Debug.Log("arenacombat state p.1 ok");
            target = unitTargetPicker.target;
            //Debug.Log($"targetpicker target"+ unitTargetPicker.target.name);
            //Debug.Log($"Target is: "+ target.name);
            //target.gameObject.GetComponent<ObjectInfo>().TellInfo();
        }
        //this is AttackTargetInRangeOrMoveTOTarget(); but done better
        AttackOrMoveToTarget();
    }
}
