using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    UnitAi unitAi;
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
        //do damage
        //StartCoroutine(Wait());
        characterAnimation.Slash();
        
        target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        StartCoroutine(Wait());

    }
    public IEnumerator AttackHit(GameObject target)
    {
        unitAi.activity = Activity.IDLE;
        characterAnimation.Slash();
        target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        yield return new WaitForSeconds(2f);
        unitAi.activity = Activity.FIGHT;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }
}
