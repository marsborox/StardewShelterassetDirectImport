using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;


public class UnitCombat : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    UnitAiBase unitAiBase;
    UnitStatsAndInfo unitStatsAndInfo;

    GameObject _target;

    public bool attackOnCD;//move this to combat

    // Start is called before the first frame update
    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        unitAiBase = GetComponent<UnitAiBase>();
        unitStatsAndInfo = GetComponent<UnitStatsAndInfo>();
    }
    void Start()
    {
        attackOnCD = false;
        //_inCombat = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack2(GameObject target)
    {//discontinued
        target.gameObject.GetComponent<UnitHealth>().TakeDamage(unitStatsAndInfo.damage);
        characterAnimation.Slash();
    }
    public void AttackHit(GameObject target)
    {//attack pre animation
     //do damage
     //StartCoroutine(Wait());
        //unitAiBase.activity = Activity.COMBAT;// *********************************
        attackOnCD = true;// **********************************
        _target = target;
        target.gameObject.GetComponent<UnitAiBase>().attacker = this.gameObject; //******************
        //target.gameObject.GetComponent<UnitAi>().target = this.gameObject;
        
        target.gameObject.GetComponent<UnitAiBase>().IfImIdleMakeMeCombat(); //********************

        unitAiBase.inCombat = true;
        characterAnimation.Idle();
        characterAnimation.Slash();
        //unitAi.target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        //unitAi.activity= Activity.IDLE;
    }

    public void AttackAnimationEvent()
    {
        _target.gameObject.GetComponent<UnitHealth>().TakeDamage(unitStatsAndInfo.damage);
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
    
}
