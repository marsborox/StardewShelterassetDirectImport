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
    GameObject target;
    UnitCombat unitCombat;
    private void Awake()
    {
        goRandomly = GetComponent<GoRandomly>();
        unitTargetPicker = GetComponent<UnitTargetPicker>();
        objectInfo = GetComponent<ObjectInfo>();
        unitMovement = GetComponent<UnitMovement>();
        unitCombat = GetComponent<UnitCombat>();
    }
    void Start()
    {
       
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
                    // ************* set source of dmg as target, if status idle change status to fight

                    break;
                }
        }
    }
    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.gameObject == target)
        {
            Debug.Log("Collided with enemy");
            unitCombat.Attack(target);
            Debug.Log("I have attacked the enemy");
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