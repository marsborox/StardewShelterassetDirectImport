using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;
using Assets.PixelFantasy.PixelTileEngine.Scripts;

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Activity { IDLE, LOCATIONACTIVITY, ARENACOMBAT, ARENARESOURCE, MOVEMENT,RANDOMAUTOMOVE,TRAVELING, KILLINGMOBS}
public enum CombatState { MOVINGTOTARGET, FIGHTING, RESTING, LOOTING, NONE }

public class UnitAi : MonoBehaviour
{
    
    //must be value so we start with something to compare
    //public int oldTargetIndex;
    public int newTargetIndex;

    public Activity activity;
    public CombatState combatState;

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
    bool _inCombat;


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
    }

    private void Update()
    {
            switch (activity)
            {
                case Activity.RANDOMAUTOMOVE:
                    {//random automovement in base area for initial testing only
                        RandomAutoMove();
                        
                        break;
                    }
                case Activity.KILLINGMOBS:
                    {
                        KillingMobs();
                    break;
                }
                case Activity.IDLE:
                    {
                        //characterAnimation.Idle();
                        // ************* set source of dmg as target, if status idle change status to fight
                        break;
                    }
            }
    }
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

    void RandomAutoMove()
    {
        goRandomly.isAi = true;
        goRandomly.goRandomly = true;
        goRandomly.GoRandomlyIfShouldAndNotPaused();
    }
    void KillingMobs()
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

        AttackTargetInRangeOrMoveTOTarget();
        //problem here
        /*
        unitMovement.Move(target.transform);
        */
        //Debug.Log("arenacombat state p.2 ok");

        
    }
    void AttackTargetInRangeOrMoveTOTarget()
    {
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