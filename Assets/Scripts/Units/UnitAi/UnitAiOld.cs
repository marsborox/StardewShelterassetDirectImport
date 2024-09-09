using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;
using Assets.PixelFantasy.PixelTileEngine.Scripts;

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public enum TaskOld { /*IDLE,*/ LOCATIONACTIVITY, COMBAT, ARENARESOURCE, MOVEMENT,RANDOMAUTOMOVE,TRAVELING, ADVENTURING, OTHER,ENEMY }//only heros will need this
public enum EnemyType {HUMANOID,MONSTER,BEAST }//only for mobs will be reworked
public enum ActivityOld { IDLE, COMBAT, RESTING,MOVING,OTHER, }
//public enum CombatState { MOVINGTOTARGET, DOINGHIT, RESTING, LOOTING, NONE, DEAD }

public class UnitAiOld : MonoBehaviour
{
    //must be value so we start with something to compare
    //public int oldTargetIndex;
    public int newTargetIndex;

    public TaskOld taskOld;
    public ActivityOld activityOld;
    public EnemyType enemyType;
    
    //public CombatState combatState;

    GoRandomly goRandomly;
    UnitTargetPicker unitTargetPicker;
    UnitMovement unitMovement;
    ObjectInfo objectInfo;

    UnitHealth unitHealth;
    UnitCombat unitCombat;
    CharacterAnimation characterAnimation;
    UnitStatsAndInfo unitStatsAndInfo;

    //can be public?
    public GameObject target;//who we attack or mine or use
    public GameObject attacker;//who attacked us

    public bool attackOnCD;
    [SerializeField] public bool inCombat;
    
    bool _targetInRange;

    private void Awake()
    {
        goRandomly = GetComponent<GoRandomly>();
        unitTargetPicker = GetComponent<UnitTargetPicker>();
        objectInfo = GetComponent<ObjectInfo>();
        unitMovement = GetComponent<UnitMovement>();
        unitCombat = GetComponent<UnitCombat>();
        unitHealth = GetComponent<UnitHealth>();
        characterAnimation = GetComponent<CharacterAnimation>();
        unitStatsAndInfo = GetComponent<UnitStatsAndInfo>();
    }
    void Start()
    {
        //activity = Activity.FIGHT;
        attackOnCD = false;
        _targetInRange = false;
        inCombat = false;

    }

    private void Update()
    {
        if (unitHealth.healthState != HealthState.DEAD)
        {
            TaskSwitch();
            if (false)
            {//will be worked on later
                //IsInCombat();
                if (inCombat)
                {
                    //CombatSwitch();
                    KillingEnemies();
                }
                else
                {
                    TaskSwitch();
                }
            }
        }
    }

    void TaskSwitch()
    {
        switch (taskOld)
        {

            case TaskOld.ADVENTURING:
                {
                    if (activityOld == ActivityOld.RESTING)
                    {
                        unitHealth.Resting2();
                    }
                    else if ((unitHealth.healthState == HealthState.LOW) && !inCombat)
                    {
                        //unitHealth.Resting();
                        activityOld = ActivityOld.RESTING;
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
            
            case TaskOld.OTHER:
                {
                    break;
                }
            case TaskOld.COMBAT:
                {
                    
                    break;
                }
            case TaskOld.ENEMY:
                {
                    EnemyBehavior();
                    break;
                }
        }
    }
    /*
    void IsInCombat()
    {
        inCombat = (!(attacker=null));
    }*/
    void ActivitySwitch()
    { 
        
    }
    void Combat()
    {
        if (target == null)
        {
            target = attacker;
        }
        AttackOrMoveToTarget();
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
        AttackTargetInRangeOrMoveTOTarget();
    }

    void AttackOrMoveToTarget()
    {//universal done
        unitMovement.TurnCorrectDirection(target.transform);
        CheckIfTargetInRange();
        if (target != null && !attackOnCD)
        { 
            if (_targetInRange)
            {
                unitCombat.AttackHit(target);
            }
            else
            {
                unitMovement.Move(target);
            }
        }
    }
    void CheckIfTargetInRange()
    { //universal done
        _targetInRange= Vector2.Distance(this.transform.position, target.transform.position) < unitStatsAndInfo.range;
    }

    public void IfImIdleMakeMeCombat()
    {//only enemies
        if (activityOld == ActivityOld.IDLE)
        {
            activityOld = ActivityOld.COMBAT;
        }
        if (activityOld == ActivityOld.RESTING)
        {
            activityOld = ActivityOld.COMBAT;
        }
    }
    void AttackTargetInRangeOrMoveTOTarget()
    {//this is old will be discontinued
        if (target != null && !attackOnCD)
        {
            //must be divided to different methods in future - ust check range if within range...
            float distance = Vector2.Distance(this.transform.position, target.transform.position);
            if (distance > unitStatsAndInfo.range)
            {
                unitMovement.Move(target);
            }

            else if (distance < unitStatsAndInfo.range)
            {
                unitCombat.AttackHit(target);
            }
        }
    }
    public void TargetDied()
    {//universal done
        target = null;
        attacker = null;
        inCombat = false;
        if (objectInfo.type == "EnemyType")
        {
            activityOld = ActivityOld.IDLE;
            unitHealth.healthCurrent = unitHealth.healthMax;
        }
    }
    public void EnemyBehavior()
    {
        //only enemy //we will have to do 
        switch (enemyType)
        {
            case EnemyType.HUMANOID:
                {
                    EnemyActivitySwitch();
                    //if we are low health call friends
                    //bcs FUCK the player Mrgllll glrrm gl!
                    break;
                }
            case EnemyType.BEAST:
                {
                    EnemyActivitySwitch();
                    break;
                }
        }
    
    }
    public void EnemyActivitySwitch()
    {//only enemy
        switch (activityOld)

        {
            case ActivityOld.IDLE:
                {
                    characterAnimation.Idle();
                    break;
                }
            case ActivityOld.COMBAT:
                {
                    Combat();
                    break;
                }
        }
    }
}



//testing methods
/*
if (targetTransform == null)
{
    Debug.Log("Target Transform is null.");
    return;
}
if (unitMovement == null)
{
    Debug.Log("unitMovement is not assigned.");
    return;
}*/


//discontinued methods
/*
void OnTriggerEnter2D(Collider2D otherCollider)
{
    if (otherCollider.gameObject == target)
    {
        unitCombat.Attack(target);
        //unitCombat.AttackPreAnimation(target);
    }
}
*/







