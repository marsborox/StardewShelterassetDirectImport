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
    UnitAiBase unitAiBase;
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
        unitAiBase = GetComponent<UnitAiBase>();
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
        if (unitAiBase.target = null)
        {
            unitAiBase.target = unitAiBase.attacker;
        }
        if (!unitAiBase.inCombat)
        { unitAiBase.inCombat = true; }
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
        unitAiBase.activity = Activity.RESTING;
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
            unitAiBase.activity = Activity.OTHER;
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
            unitAiBase.activity = Activity.OTHER;
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
        unitAiBase.attacker.GetComponent<UnitAiBase>().TargetDied();
        unitAiBase.target = null;
        unitAiBase.attacker = null;
        unitAiBase.inCombat = false;
        //gameObject.GetComponent<UnitAiBase>().task = Task.OTHER; //*************************
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
