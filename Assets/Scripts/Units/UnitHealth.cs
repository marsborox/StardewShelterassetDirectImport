using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;


public enum HealthState { FULL,LOW,DEAD, ALIVE}

public class UnitHealth : MonoBehaviour
{
//fuckallhasbeendoneonthisday
    [SerializeField] public int healthMax;
    [SerializeField] public int healthCurrent;
    [SerializeField] float healthFraction;
    [SerializeField] public bool isResting;
    [SerializeField] public bool healthLow;
    [SerializeField] public bool healthFull;
    [SerializeField] public bool _restingTick;
    [SerializeField] Image healthBarSprite;

    UnitStatsAndInfo unitStatsAndInfo;
    UnitAiOld unitAi;
    CharacterAnimation characterAnimation;
    ObjectInfo objectInfo;

    public HealthState healthState;

    /*
    [SerializeField] public bool isResting { get; private set; }
    [SerializeField] public bool healthLow { get; private set; }
    [SerializeField] public bool healthFull { get; private set; }
    */
    public int lowHPTresholdPercentage;
    float _despawnTime=5f;
    float _restingTickHealPercentage = 20;
    float _lastRestingTickTime;
    float _restingTickCD = 5f;
    float _restingHealPerSecond;
    //Activity activity;
    // Start is called before the first frame update
    IEnumerator restingRoutine;
    
    private void Awake()
    {
        unitStatsAndInfo=GetComponent<UnitStatsAndInfo>();
        unitAi=GetComponent<UnitAiOld>();
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
        _lastRestingTickTime=Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        AliveDeadSwitch();
    }
    void AliveDeadSwitch()
    {
        if (healthState != HealthState.DEAD)
        {
            ControlHealthBarSize();
            CheckHP2();
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
    
    public void Resting()
    {
        if (!isResting)
        {
            CalcRestingHeal();
        }
        isResting = true;
        unitAi.activityOld = ActivityOld.RESTING;
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
            unitAi.activityOld = ActivityOld.OTHER;
        }
    }
    
    public void Resting2()
    {
        characterAnimation.Crouch();
        if (!_restingTick)
        {
            CalcRestingHeal();
            restingRoutine = RestingHealPerTick();
            StartCoroutine(restingRoutine);
        }
        if (healthState == HealthState.FULL)
        {
            StopCoroutine(restingRoutine);
            _restingTick = false;
            unitAi.activityOld = ActivityOld.OTHER;
        }
    }
    private IEnumerator RestingHealPerTick()
    {
        _restingTick = true;
        yield return new WaitForSeconds(_restingTickCD);
        healthCurrent = healthCurrent + (int)Mathf.Round(_restingHealPerSecond*_restingTickCD);
        _restingTick = false;
    }
    void CalcRestingHeal()
    { //5% per second
        _restingHealPerSecond = healthMax / _restingTickHealPercentage;
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
    void CheckHP2()
    {
        if (healthCurrent >= healthMax)
        {
            healthCurrent = healthMax;
            healthState = HealthState.FULL; 
        }
        else if (healthCurrent < 1)
        {
            Die();
            
        }
        else if ((((float)healthCurrent / (float)healthMax) * 100) < lowHPTresholdPercentage)
        { healthState = HealthState.LOW; }

        else
        { healthState = HealthState.ALIVE; }
    }
    public void Die()
    {
        gameObject.tag = ("DeadEnemyUnit");
        Debug.Log("I am dying");
        unitAi.attacker.GetComponent<UnitAiOld>().TargetDied();
        unitAi.target = null;
        unitAi.attacker = null;
        unitAi.inCombat = false;
        gameObject.GetComponent<UnitAiOld>().taskOld = TaskOld.OTHER;
        healthState = HealthState.DEAD;
        StopAllCoroutines();
        this.characterAnimation.Die();
        StartCoroutine(Despawnm());
    }
    public IEnumerator Despawnm()
    {
        yield return new WaitForSeconds(_despawnTime);
        Destroy(this.gameObject);
        gameObject.transform.parent.transform.parent.GetComponent<ObjectSpawner>().UnitDied();
    }

}
