using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;

using UnityEngine;
using UnityEngine.Pool;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Max y and x axis of area")]
    [SerializeField] float maxXaxisOfArea;
    [SerializeField] float maxYaxisOfArea;
    // x-11,11; y-8,8 
    [Header("arena settings and prefabs for spawning")]
    [SerializeField] GameObject enemyUnit;
    [SerializeField] GameObject lootChest;
    [SerializeField] GameObject heroUnit;
    [SerializeField] GameObject heroCamp;

    [Header("ScriptableObjects")]
    [SerializeField]  ArenaSettingSO _arenaSetting;
    UnitRaceSO _spawnedEnemyUnitRaceSO;
    UnitSO spawnedEnemyUnitSO;
    [SerializeField]  UnitSO spawnedHeroSO;


    //[SerializeField] int maxObjectsInArea = 10;
    //[SerializeField] int maxLootChestsInArea = 6;
    //[SerializeField] int maxEnemyUnitsInArea = 8;


    [Header("counters just to control")]
    [SerializeField] public int spawnedEnemyUnits;
    [SerializeField] public int spawnedLootChests;
    [SerializeField] public int totalSpawnedObjects;

    GameObject spawnedUnit;
    GameObject spawnedGameObject;
    GameObject spawnedHero;

    private ObjectPool<GameObject> _spawnedObjectPool;
    //ObjectInfo objectInfo;
    //this is name of parent object of arena
    [SerializeField] public GameObject combatAreaSpawn;//unused only for centre

    static List<GameObject> spawnedGameObjectList = new List<GameObject>();

    int nameCounter = 0;

    public delegate void SpawnEvent(GameObject gameObject);

    public static SpawnEvent OnMobSpawn, OnHeroSpawn;
    
    private void Awake()
    {
        
        //objectInfo = FindObjectOfType<ObjectInfo>();
    }
    private void Start()
    {
        SetSOsSettings();
        SpawnHeroOnCamp();
        CreateGameObjectPool();
    }
    private void Update()
    {
        SpawnObjectsOnMap();
    }
    private void OnEnable()
    {
        OnMobSpawn += AddClassAiMobs;
        OnHeroSpawn += AddClassAiHeros;
    }
    private void OnDisable()
    {
        OnMobSpawn -= AddClassAiMobs;
        OnHeroSpawn -= AddClassAiHeros;
    }
    public void CreateGameObjectPool()
    {



    }
        public void UnitDied()
    {
        spawnedEnemyUnits--;
        totalSpawnedObjects--;
    }
    void SpawnHeroOnCamp()
    {//temp method will remove later


        spawnedHero = Instantiate(heroUnit);
        OnHeroSpawn?.Invoke(spawnedHero);

        spawnedHero.GetComponent<ObjectInfo>().SetType("HeroUnit");
        spawnedHero.gameObject.tag = "HeroUnit";
        spawnedHero.GetComponent<UnitTargetPicker>().tagOfEnemy = "EnemyUnit";
        spawnedHero.transform.parent = combatAreaSpawn.transform;
        spawnedHero.transform.position = heroCamp.transform.position;
        SetHeroStats();
        spawnedHero.GetComponent<UnitAiHeros>().task = Task.ADVENTURING;
    }
    void SpawnMobs()
        //***********************************************************
    {//rework, we must attach all scripts in different way, problem with class initiation
        //chatgpt got us usggestions
        //mabye doing it as event would be solution
        //**********************************************************

        SpawnObjectRandomly(enemyUnit);
        OnMobSpawn?.Invoke(spawnedGameObject);

        //SetNameAndCounters();
        SetUnitRace();
        SetUnitVisuals();
        SetUnitClass();
        SetUnitStats();
        SetUnitTypeTagCounters();
    }

    //*************************************** methods that will subscribe to events
    
    void AddClassAiMobs(GameObject gameObject)
    { gameObject.AddComponent<UnitAiMobs>(); }
    void AddClassAiHeros(GameObject gameObject)
    { 
        gameObject.AddComponent<UnitAiHeros>();
        gameObject.AddComponent<Inventory>();
        
    }
    
    void SpawnObjectsOnMap()
    {
        //GameObject spawningObject;
        if (totalSpawnedObjects < _arenaSetting.maxObjectsInArea)
        {
            int random = UnityEngine.Random.Range(0, 2);
            //Debug.Log(random);
            if (random == 0 && spawnedEnemyUnits < _arenaSetting.maxEnemyUnitsInArea)
            {
                //SpawnObjectRandomlyOrig(enemyUnit);
                SpawnMobs();
                //spawn Unit
                //attempt to do it as event not good idea at all
                //SpawnObjectRandomly(enemyUnit);
                //OnEnemySpawn?.Invoke();
            }
            else if (random == 1 && spawnedLootChests < _arenaSetting.maxLootChestsInArea)
            {
                //SpawnObjectRandomlyOrig(lootChest);
                SpawnLootChest();
                //spawn
            }
        }
        else return;
    }
    void SpawnLootChest()
    {
        SpawnObjectRandomly(lootChest);
        SetLootChestCounters();
    }
    void SpawnObjectRandomly(GameObject gameObject)
    {
        spawnedGameObject = Instantiate(gameObject);

        //this will set parent object
        spawnedGameObject.transform.parent = combatAreaSpawn.transform;

        //random place in Map relative to parent object (centre)
        float vectorX = (UnityEngine.Random.Range(-maxXaxisOfArea, maxXaxisOfArea) + combatAreaSpawn.transform.position.x);
        float vectorY = (UnityEngine.Random.Range(-maxYaxisOfArea, maxYaxisOfArea) + combatAreaSpawn.transform.position.y);
        float vectorZ = 0f;

        Vector3 spawnPointVector = new Vector3(vectorX, vectorY, vectorZ);
        //selfQuaternion  needed for vector3
        //Quaternion rotation = Quaternion.identity;

        //place where we want him
        spawnedGameObject.transform.position = spawnPointVector;
        //Debug.Log("shouldHaveSpawn " + vectorX + " " + vectorY);

        //float realX = spawnedGameObject.transform.position.x;
        //float realY = spawnedGameObject.transform.position.y;
        //Debug.Log("spawnedin "+realX +" " +realY );

        // set name and type
    }
    void SetUnitRace()
    {
        //CharacterBuilder characterBuilder;
        spawnedGameObject.GetComponent<CharacterBuilder>().Head = _spawnedEnemyUnitRaceSO.race;
        spawnedGameObject.GetComponent<CharacterBuilder>().Ears = _spawnedEnemyUnitRaceSO.race;
        spawnedGameObject.GetComponent<CharacterBuilder>().Eyes = _spawnedEnemyUnitRaceSO.race;
        spawnedGameObject.GetComponent<CharacterBuilder>().Body = _spawnedEnemyUnitRaceSO.race;
    }
    void SetUnitVisuals()
    {
        spawnedGameObject.GetComponent<CharacterBuilder>().Hair = "";
    }
    void SetUnitClass()
    {
        //armor set here
        spawnedGameObject.GetComponent<CharacterBuilder>().Rebuild();//this will reload visuals
    }
    void SetUnitStats()
    {
        spawnedGameObject.GetComponent<UnitStatsAndInfo>().unitSettings = spawnedEnemyUnitSO;
        spawnedGameObject.GetComponent<UnitStatsAndInfo>().SetStats();
    }
    void SetHeroStats()
    {
        //temp method for testing will be removed
        spawnedHero.GetComponent<UnitStatsAndInfo>().unitSettings = spawnedHeroSO;
        spawnedHero.GetComponent<UnitStatsAndInfo>().SetStats();
    }
    void SetUnitTypeTagCounters()
    {
        spawnedGameObject.GetComponent<ObjectInfo>().SetType("EnemyUnit");
        spawnedGameObject.gameObject.tag = "EnemyUnit";
        spawnedGameObject.GetComponent<UnitTargetPicker>().tagOfEnemy = "HeroUnit";
        spawnedEnemyUnits++;
        totalSpawnedObjects++;
        
    }
    void SetLootChestCounters()
    {
        spawnedLootChests++;
        totalSpawnedObjects++;
    }

    



    /*
    void SetEnemyUnitStates()
    {
        spawnedGameObject.GetComponent<UnitAiMobs>().task = Task.ENEMY;
        
        EnemyTypeEnum(_spawnedEnemyUnitRaceSO.enemyType);
    }
    void EnemyTypeEnum(EnemyType enemy)
    {
        spawnedGameObject.GetComponent<UnitAiMobs>().enemyType = enemy;
    }
    */
    void AddSpawnedObjectToList()
    {
        spawnedGameObjectList.Add(spawnedUnit);
    }
    void SpawnLootCrate()
    {
        Instantiate(lootChest);
        spawnedLootChests++;
        totalSpawnedObjects++;
    }
    void SetSOsSettings()
    {
        _spawnedEnemyUnitRaceSO= _arenaSetting.unitRaceSO;
        spawnedEnemyUnitSO= _arenaSetting.unitSettingSO;
    }
    /*void SetInstantiatedObjectName(GameObject gameObject)
{
    string name;
    name = gameObject.GetComponent<ObjectInfo>().name + nameCounter;
    nameCounter++;
    ObjectInfo objectInfo;
    objectInfo = FindObjectOfType<ObjectInfo>();
    objectInfo.SetName(name);
    Debug.Log(name);

    string type = objectInfo.GetType();

    if (type == "Unit")
    {
        objectInfo.SetType("Enemy");
        spawnedEnemyUnits++;
        totalSpawnedObjects++;
    }
    else if(type == "LootChest")
    {
        spawnedLootChests++;
        totalSpawnedObjects++;
    }*/

    void CheckObjectsInArena()
    {

    }
    void GiveMeRandomTransforms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float x;
            float y;
            x = UnityEngine.Random.Range(-maxXaxisOfArea, maxXaxisOfArea);
            y = UnityEngine.Random.Range(-maxYaxisOfArea, maxYaxisOfArea);
            Debug.Log(x + "  " + y);
        }
    }

    /*void SpawnMeSomeUnits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnUnitRandomly();
        }
    }*/

}

/*void SetNameAndCounters() //discontinuing
{
    string name;
    name = spawnedGameObject.GetComponent<ObjectInfo>().name + nameCounter;
    nameCounter++;
    ObjectInfo objectInfo;
    objectInfo = FindObjectOfType<ObjectInfo>();
    objectInfo.SetName(name);
    //Debug.Log(name);
    string type = objectInfo.GetType();

    if (type == "Unit")
    {
        objectInfo.SetType("Enemy");

        spawnedEnemyUnits++;
        totalSpawnedObjects++;
    }
    else if (type == "LootChest")
    {
        spawnedLootChests++;
        totalSpawnedObjects++;
    }

    //spawnedGameObjectList.Add(gameObject.name,gameObject.GetType});
    //add to list
}*/
/*void SpawnObjectRandomlyOrig(GameObject gameObject)
{//discontinued
    spawnedGameObject = Instantiate(gameObject);

    //this will set parent object
    spawnedGameObject.transform.parent = combatAreaSpawn.transform;

    //random place in Map relative to parent object (centre)
    float vectorX = (UnityEngine.Random.Range(-maxXaxisOfArea, maxXaxisOfArea) + combatAreaSpawn.transform.position.x);
    float vectorY = (UnityEngine.Random.Range(-maxYaxisOfArea, maxYaxisOfArea) + combatAreaSpawn.transform.position.y);
    float vectorZ = 0f;

    Vector3 spawnPointVector = new Vector3(vectorX, vectorY, vectorZ);
    //selfQuaternion  needed for vector3
    //Quaternion rotation = Quaternion.identity;

    //place where we want him
    spawnedGameObject.transform.position = spawnPointVector;
    //Debug.Log("shouldHaveSpawn " + vectorX + " " + vectorY);

    //float realX = spawnedGameObject.transform.position.x;
    //float realY = spawnedGameObject.transform.position.y;
    //Debug.Log("spawnedin "+realX +" " +realY );

    // set name and type

    string name;
    name = gameObject.GetComponent<ObjectInfo>().name + nameCounter;
    nameCounter++;
    ObjectInfo objectInfo;
    objectInfo = FindObjectOfType<ObjectInfo>();
    objectInfo.SetName(name);
    //Debug.Log(name);
    string type = objectInfo.GetType();

    if (type == "Unit")
    {
        objectInfo.SetType("Enemy");

        spawnedEnemyUnits++;
        totalSpawnedObjects++;


    }
    else if (type == "LootChest")
    {
        spawnedLootChests++;
        totalSpawnedObjects++;
    }

    //spawnedGameObjectList.Add(gameObject.name,gameObject.GetType});
    //add to list
}*/
