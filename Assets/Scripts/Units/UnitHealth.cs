using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public enum HealthState { ALIVE, DEAD }

public class UnitHealth : MonoBehaviour
{
//fuckallhasbeendoneonthisday
    [SerializeField] public int healthMax;
    [SerializeField] public int healthCurrent;
    [SerializeField] float healthFraction;
    [SerializeField] Image healthBarSprite;

    UnitStatsAndInfo unitStatsAndInfo;
    UnitAi unitAi;
    CharacterAnimation characterAnimation;
    ObjectInfo objectInfo;
    float _despawnTime=5f;
    float _restingTickTime = 5f;
    public int lowHPTresholdPercentage;
    float _restingHealPerSecond;

    public HealthState healthState;

    [SerializeField] public bool isResting;
    [SerializeField] public bool healthLow;
    [SerializeField] public bool healthFull;
    /*
    [SerializeField] public bool isResting { get; private set; }
    [SerializeField] public bool healthLow { get; private set; }
    [SerializeField] public bool healthFull { get; private set; }
    */
    [SerializeField] public bool _restingTick;
    //Activity activity;
    // Start is called before the first frame update
    IEnumerator restingRoutine;
    
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
        healthLow = false;
        isResting = false;
        _restingTick = false;
        lowHPTresholdPercentage = 40;//must be done better
    }

    // Update is called once per frame
    void Update()
    {
        AliveDeadSwitch();
        if (isResting&&unitAi.activity != Activity.RESTING ) 
        {
            _restingTick = false;
            StopCoroutine(restingRoutine);
            _restingTick = false;
            Debug.Log("Rest Done1");
            //isResting=false;
        }
   
    }
    void AliveDeadSwitch()
    {
        switch (healthState)
        {
            case HealthState.ALIVE:
                {
                    ControlHealthBarSize();
                    CheckHP();
                    break;
                }
            case HealthState.DEAD: 
                {

                    break;
                }
        }
    }

    public void TakeDamage(int damage)
    {

        healthCurrent -= damage;
        if (unitAi.target = null)
        { 
            unitAi.target = unitAi.attacker;
        }
        if (!unitAi.inCombat)
        { unitAi.inCombat = true; }
    }


    void ControlHealthBarSize()
    {
        healthFraction = (float)healthCurrent / (float)healthMax;
        healthBarSprite.fillAmount = healthFraction;
    }
    public IEnumerator Despawnm()
    {
        yield return new WaitForSeconds(_despawnTime);
        Destroy(this.gameObject);
        gameObject.transform.parent.transform.parent.GetComponent<ObjectSpawner>().UnitDied();
    }

    public void Resting()
    {
        if (!isResting)
        {
            CalcRestingHeal();
        }
        isResting = true;
        unitAi.activity = Activity.RESTING;
        characterAnimation.Crouch();
        if (!_restingTick)
        {
            restingRoutine = RestingHealPerTick();
            StartCoroutine(restingRoutine);
        }

        if (healthFull)
        {
            isResting = false;
            _restingTick = false;
            StopCoroutine(restingRoutine);
            unitAi.activity = Activity.OTHER;
        }
        
    }
    private IEnumerator RestingHealPerTick()
    {
        _restingTick = true;
        yield return new WaitForSeconds(_restingTickTime);
        healthCurrent = healthCurrent + (int)Mathf.Round(_restingHealPerSecond*_restingTickTime);
        _restingTick = false;
    }
    void CalcRestingHeal()
    { //5% per second
        _restingHealPerSecond = healthMax / 20;
    }
    void CheckHP()
    {
        healthLow = (( (float)healthCurrent/ (float)healthMax)*100)<lowHPTresholdPercentage;
        healthFull = (healthCurrent >= healthMax);
        if (healthCurrent > healthMax)
        { 
            healthCurrent= healthMax;
        }
        if (healthCurrent < 1)
        { 
            Die();
        }
    }
    public void Die()
    {
        gameObject.tag = ("DeadEnemyUnit");


        Debug.Log("I am dying");

        unitAi.attacker.GetComponent<UnitAi>().TargetDied();

        unitAi.target = null;
        unitAi.attacker = null;
        unitAi.inCombat = false;
        gameObject.GetComponent<UnitAi>().task = Task.OTHER;
        healthState = HealthState.DEAD;
        StopAllCoroutines();
        this.characterAnimation.Die();

        StartCoroutine(Despawnm());

    }

}
