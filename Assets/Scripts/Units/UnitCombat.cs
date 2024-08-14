using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    CharacterAnimation characterAnimation;
    // Start is called before the first frame update
    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
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
        
        characterAnimation.Slash();
        target.gameObject.GetComponent<UnitHealth>().TakeDamage();
        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
    }
}
