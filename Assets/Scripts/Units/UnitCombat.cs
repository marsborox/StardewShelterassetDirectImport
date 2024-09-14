using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public enum CombatActivity { IDLE, COMBAT, RESTING, MOVING, OTHER, }
public class UnitCombat : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    
    UnitStatsAndInfo unitStatsAndInfo;
    UnitMovement unitMovement;
    ObjectInfo objectInfo;
    UnitHealth unitHealth;
    UnitTargetPicker unitTargetPicker;

    CombatActivity CombatActivity;

    [SerializeField] GameObject _target;

    public bool attackOnCD;

    public bool inCombat;
    public bool _targetInRange;
    public GameObject target;
    public GameObject attacker;//who attacked us
    // Start is called before the first frame update
    private void Awake()
    {
        unitHealth = GetComponent<UnitHealth>();
        characterAnimation = GetComponent<CharacterAnimation>();
        objectInfo = GetComponent<ObjectInfo>();
        unitStatsAndInfo = GetComponent<UnitStatsAndInfo>();
        unitMovement = GetComponent<UnitMovement>();
        unitTargetPicker = GetComponent<UnitTargetPicker>();
    }
    void Start()
    {
        inCombat = false;
        attackOnCD = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack2(GameObject target)
    {//discontinued
        target.gameObject.GetComponent<UnitCombat>().TakeDamage(unitStatsAndInfo.damage);
        characterAnimation.Slash();
    }
    public void AttackHit(GameObject target)
    {//attack pre animation
     //do damage
     //StartCoroutine(Wait());
     //unitAiBase.activity = Activity.COMBAT;// *********************************
        //Debug.Log("DOING ATTACK HIT");
        attackOnCD = true;// **********************************
        _target = target;//problem here
        //Debug.Log("local _target set");
        //Debug.Log(_target);
        target.gameObject.GetComponent<UnitCombat>().attacker = this.gameObject; //mabye problem here too
        //target.gameObject.GetComponent<UnitAi>().target = this.gameObject;
        
        target.gameObject.GetComponent<UnitAiBase>().IfImIdleMakeMeCombat(); //********************

        inCombat = true;
        characterAnimation.Idle();
        characterAnimation.Slash();
        //unitAi.target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        //unitAi.activity= Activity.IDLE;
    }

    public void AttackAnimationEvent()
    {
        _target.gameObject.GetComponent<UnitCombat>().TakeDamage(unitStatsAndInfo.damage);
        _target = null;
        StartCoroutine(AttackCooldown());
    }
    public IEnumerator AttackCooldown()
    {
        //after slash is performed
        //unitAi.activity = Activity.IDLE;

        
        yield return new WaitForSeconds(unitStatsAndInfo.attackSpeed);
        attackOnCD = false; // *********************************
        //unitAi.activity = Activity.IDLE;
    }
    public void AttackOrMoveToTarget() //move to combat
    {
        unitMovement.TurnCorrectDirection(target.transform);
        CheckIfTargetInRange();
        if (target != null && !attackOnCD)
        {
            if (_targetInRange)
            {
                AttackHit(target);
            }
            else
            {
                unitMovement.Move(target);
            }
        }
    }
    public void TargetDied()
    {
        target = null;
        attacker = null;
        inCombat = false;
        if (objectInfo.type == "EnemyUnit")
        {
            CombatActivity = CombatActivity.IDLE;
            unitHealth.healthCurrent = unitHealth.healthMax;
        }
    }
    void CheckIfTargetInRange()
    {
        _targetInRange = Vector2.Distance(this.transform.position, target.transform.position) < unitStatsAndInfo.range;
    }

    public void KillingEnemies()
    {
        //Debug.Log("Killing ENemies Method started");
        if (target == null)
        {
           //Debug.Log("target is NULL");
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
    public void TakeDamage(int damage)
    {
        unitHealth.healthCurrent -= damage;
        if (target = null)
        {
            target = attacker;
        }
        if (!inCombat)
        { inCombat = true; }
    }
    public void Combat()
    {
        if (target == null)
        {
            target = attacker;
        }
        AttackOrMoveToTarget();
    }
}
