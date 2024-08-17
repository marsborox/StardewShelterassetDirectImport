using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Activity { IDLE, LOCATIONACTIVITY, ARENACOMBAT, ARENARESOURCE, MOVEMENT,RANDOMAUTOMOVE,TRAVELING, FIGHT}

public class UnitAi : MonoBehaviour
{
    
    //must be value so we start with something to compare
    //public int oldTargetIndex;
    public int newTargetIndex;

    public Activity activity;

    GoRandomly goRandomly;
    UnitTargetPicker unitTargetPicker;
    UnitMovement unitMovement;
    ObjectInfo objectInfo;
    //can be public?
    GameObject target;
    UnitCombat unitCombat;
    CharacterAnimation characterAnimation;
    private void Awake()
    {
        goRandomly = GetComponent<GoRandomly>();
        unitTargetPicker = GetComponent<UnitTargetPicker>();
        objectInfo = GetComponent<ObjectInfo>();
        unitMovement = GetComponent<UnitMovement>();
        unitCombat = GetComponent<UnitCombat>();
        characterAnimation = GetComponent<CharacterAnimation>();
    }
    void Start()
    {
       //activity = Activity.FIGHT;
    }

    private void Update()
    {
        switch (activity) 
        {
            case  Activity.RANDOMAUTOMOVE:
                {
                    goRandomly.isAi=true;
                    goRandomly.goRandomly = true;
                    goRandomly.GoRandomlyIfShouldAndNotPaused();
                    break;
                }
            case Activity.FIGHT:
                {
                    //Debug.Log("arenacombat state");
                    if (target == null)
                    {

                        unitTargetPicker.FindClosestEnemy();
                        //Debug.Log("arenacombat state p.1 ok");
                        target = unitTargetPicker.target;
                        Debug.Log($"targetpicker target"+ unitTargetPicker.target.name);
                        Debug.Log($"Target is: "+ target.name);
                        target.gameObject.GetComponent<ObjectInfo>().TellInfo();

                    }
                    //problem here

                    unitMovement.Move(target.transform);
                    //Debug.Log("arenacombat state p.2 ok");

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
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject == target)
        {
            unitCombat.Attack(target);
            //unitCombat.AttackPreAnimation(target);
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