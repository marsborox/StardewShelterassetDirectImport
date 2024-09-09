using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;


public enum Activity { IDLE, COMBAT, RESTING, MOVING, OTHER, }
public class UnitAiBase : MonoBehaviour
{
    //universal class for all units in game
    public Activity activity;


    public UnitMovement unitMovement;
    UnitCombat unitCombat;
    UnitStatsAndInfo unitStatsAndInfo;
    ObjectInfo objectInfo;


    public UnitHealth unitHealth;
    public UnitTargetPicker unitTargetPicker;

    public GameObject target;
    public GameObject attacker;//who attacked us

    public bool attackOnCD;//move this to combat
    bool _targetInRange;
    [SerializeField] public bool inCombat;
    private void Awake()
    {
        unitMovement = GetComponent<UnitMovement>();
        unitCombat = GetComponent<UnitCombat>();
        unitStatsAndInfo = GetComponent<UnitStatsAndInfo>();
        unitHealth = GetComponent<UnitHealth>();
        unitTargetPicker = GetComponent<UnitTargetPicker>();
    }
    void Start()
    {
        attackOnCD = false;
        _targetInRange = false;
        inCombat = false;
    }

    
    void Update()
    {
        
    }

    public void AttackOrMoveToTarget()
    {
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
    {
        _targetInRange = Vector2.Distance(this.transform.position, target.transform.position) < unitStatsAndInfo.range;
    }

    public void TargetDied()
    {//universal done
        target = null;
        attacker = null;
        inCombat = false;
        if (objectInfo.type == "EnemyType")
        {
            activity = Activity.IDLE;
            unitHealth.healthCurrent = unitHealth.healthMax;
        }
    }
}
