using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;


public class UnitCombat : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    UnitAiOld unitAiOld;
    UnitStatsAndInfo unitStatsAndInfo;

    GameObject _target;

    
    
    // Start is called before the first frame update
    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        unitAiOld = GetComponent<UnitAiOld>();
        unitStatsAndInfo = GetComponent<UnitStatsAndInfo>();
    }
    void Start()
    {
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
        unitAiOld.activityOld = ActivityOld.COMBAT;
        unitAiOld.attackOnCD = true;
        _target = target;
        target.gameObject.GetComponent<UnitAiOld>().attacker = this.gameObject;
        //target.gameObject.GetComponent<UnitAi>().target = this.gameObject;
        
        target.gameObject.GetComponent<UnitAiOld>().IfImIdleMakeMeCombat();
        
        unitAiOld.inCombat = true;
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
        unitAiOld.attackOnCD = false;
        //unitAi.activity = Activity.IDLE;
    }
    
}
