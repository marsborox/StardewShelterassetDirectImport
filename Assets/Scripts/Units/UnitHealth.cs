using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
//fuckallhasbeendoneonthisday
    [SerializeField] float healthMax;
    [SerializeField] float healthCurrent;
    [SerializeField] float healthFraction;
    [SerializeField] Image healthBarSprite;

    UnitStatsAndInfo unitStatsAndInfo;
    UnitAi unitAi;
    CharacterAnimation characterAnimation;
    ObjectInfo objectInfo;
    float _despawnTime=5f;
    // Start is called before the first frame update
    private void Awake()
    {
        unitStatsAndInfo=GetComponent<UnitStatsAndInfo>();
        unitAi=GetComponent<UnitAi>();
        characterAnimation = GetComponent<CharacterAnimation>();
        objectInfo = GetComponent<ObjectInfo>();
    }
    void Start()
    {
        healthMax = unitStatsAndInfo.health;
        healthCurrent = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
        ControlHealthBarSize();
    }

    public void Die()
    {
        gameObject.tag = ("DeadEnemyUnit");
        
        

        Debug.Log("I am dying");
        unitAi.attacker.GetComponent<UnitAi>().target = null;
        
        this.characterAnimation.Die();
        StartCoroutine(Despawnm());
    }
    public void TakeDamage()
    {
        //we will pass value into this method
        //some health = health - received dmg
        //if hp =<1 die
        Die();
    }
    void ControlHealthBarSize()
    {
        healthFraction = healthCurrent / healthMax;
        healthBarSprite.fillAmount = healthFraction;
    }
    public IEnumerator Despawnm()
    {
        yield return new WaitForSeconds(_despawnTime);
        Destroy(this.gameObject);
        gameObject.transform.parent.transform.parent.GetComponent<ObjectSpawner>().UnitDied();
    }
}
