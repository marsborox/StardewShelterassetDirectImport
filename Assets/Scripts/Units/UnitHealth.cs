using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Pool;
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

    UnitStatsAndInfo unitStatsAndInfo;
    
    CharacterAnimation characterAnimation;
    ObjectInfo objectInfo;
    UnitBars unitBars;
    UnitCombat unitCombat;

    public HealthState healthState;
    


    /*
    [SerializeField] public bool isResting { get; private set; }
    [SerializeField] public bool healthLow { get; private set; }
    [SerializeField] public bool healthFull { get; private set; }
    */
    [SerializeField] Image _healthBarSprite;
    public int lowHPTresholdPercentage;
    float _despawnTime=5f;
    float _restingTickHealPercentage = 20;
    float _lastRestingTickTime;
    float _restingTickCD = 5f;
    float _restingHealPerSecond;
    //Activity activity;
    // Start is called before the first frame update
    IEnumerator restingRoutine;

    private IObjectPool<GameObject> _spawnedMobPool;
    public IObjectPool<GameObject> objectPool { set => _spawnedMobPool = value; }
    private void Awake()
    {
        
        unitStatsAndInfo=GetComponent<UnitStatsAndInfo>();
        characterAnimation = GetComponent<CharacterAnimation>();
        objectInfo = GetComponent<ObjectInfo>();
        unitCombat= GetComponent<UnitCombat>();
    }
    void Start()
    {
        //_healthBarSprite = unitBars.healthBarSprite;
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
            ControlHealthBarSize(); //seems to be broken
            CheckHP();
        }
    }

    void ControlHealthBarSize()
    {
        healthFraction = (float)healthCurrent / (float)healthMax;
        _healthBarSprite.fillAmount = healthFraction;
    }
    
    public void Resting()
    {
        if (!isResting)
        {
            CalcRestingHeal();
        }
        isResting = true;
        unitCombat.combatActivity = CombatActivity.RESTING;
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
            unitCombat.combatActivity = CombatActivity.OTHER;
        }
    }
    
    public void Resting2()
    {
        unitCombat.combatActivity = CombatActivity.RESTING;
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
            unitCombat.combatActivity = CombatActivity.OTHER;
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
        healthState = HealthState.DEAD;
        unitCombat.combatActivity=CombatActivity.DEAD;
        gameObject.tag = ("DeadEnemyUnit");
        Debug.Log("I am dying");
        unitCombat.attacker.GetComponent<UnitCombat>().TargetDied();
        //DropLoot**********************************
        unitCombat.target = null;
        unitCombat.attacker = null;
        unitCombat.inCombat = false;
        this.characterAnimation.Die();
        StopAllCoroutines();
        StartCoroutine(Despawn());
    }
    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(_despawnTime);

        //Destroy(this.gameObject);

        gameObject.transform.parent.transform.parent.GetComponent<ObjectSpawner>().MobDied();
        //with pooling m,ust reset char..
        /*
        healthCurrent = healthMax;
        healthState = HealthState.FULL;
        unitCombat.combatActivity = CombatActivity.IDLE;
        */
        _spawnedMobPool.Release(gameObject);
        //gameObject.SetActive(false);//for some reason pooling not perfect must do this too
    }
    public void Respawn()
    {
        //posiiton done on spawner
        gameObject.transform.parent.transform.parent.GetComponent<ObjectSpawner>().MobSpawned();
        
        healthCurrent = healthMax;
        healthState = HealthState.FULL;
        unitCombat.combatActivity=CombatActivity.IDLE;
        
    }
}
