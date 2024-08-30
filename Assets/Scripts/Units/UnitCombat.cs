using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;


public class UnitCombat : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    UnitAi unitAi;
    
    
    UnitStatsAndInfo unitStatsAndInfo;

    GameObject _target;

    
    
    // Start is called before the first frame update
    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        unitAi = GetComponent<UnitAi>();
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
    {
        
        target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        characterAnimation.Slash();
    }
    public void AttackHit(GameObject target)
    {//attack pre animation
     //do damage
     //StartCoroutine(Wait());

        unitAi.attackOnCD = true;
        _target = target;
        target.gameObject.GetComponent<UnitAi>().attacker = this.gameObject;
        characterAnimation.Idle();
        characterAnimation.Slash();
        //unitAi.target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        //unitAi.activity= Activity.IDLE;
            
    }

    public void AttackAnimationEvent()
    {
        _target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        _target = null;
        StartCoroutine(AttackCooldown());
    }
    public IEnumerator AttackCooldown()
    {
        //after slash is performed
        //unitAi.activity = Activity.IDLE;

        
        yield return new WaitForSeconds(unitStatsAndInfo.attackSpeed);
        unitAi.attackOnCD = false;
        //unitAi.activity = Activity.IDLE;
    }
    
}
