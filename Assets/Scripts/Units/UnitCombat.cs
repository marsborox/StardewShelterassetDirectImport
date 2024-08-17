using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public class UnitCombat : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    UnitAi unitAi;
    //private GameObject _target;
    CharacterController2D characterController;
    // Start is called before the first frame update
    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
        unitAi = GetComponent<UnitAi>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Attack(GameObject target)
    {
        
        target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        characterAnimation.Slash();
    }
    public void AttackPreAnimation(GameObject target)
    {
        //do damage
        //StartCoroutine(Wait());
        //_target = target;
        characterAnimation.Idle();
        characterAnimation.Slash();
        //unitAi.target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        
    }
    public void AttackPostAnimation(GameObject target)
    {
        //target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        //unitAi.activity = Activity.FIGHT;
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }
    public IEnumerator AttackAnimationEvent()
    {
        //after slash is performed
        //unitAi.activity = Activity.IDLE;
        yield return new WaitForSeconds(2f);
        //AttackPostAnimation(_target);
    }
}
