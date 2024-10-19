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


    //this is name of parent object of arena
    [SerializeField] public GameObject combatAreaSpawn;//objects spawn here

    static List<GameObject> spawnedGameObjectList = new List<GameObject>();

    int nameCounter = 0;
    //pool for mobs
    private IObjectPool<GameObject> _spawnedMobPool;
    //default mob pool size (to start with dont need tho
    private int _mobPoolDefaultCapacity;
    //max mob pool size - max enemy units
    private int _mobPoolMaxSize;
    // throw an exception if we try to return an existing item, already in the pool
    [SerializeField] private bool _collectionCheck = true;

    private void Awake()
    {
        _mobPoolMaxSize = _arenaSetting.maxEnemyUnitsInArea;
        //objectInfo = FindObjectOfType<ObjectInfo>();
        _spawnedMobPool = new ObjectPool<GameObject>(CreateMob, OnGetFromMobPool, OnReleaseToMobPool, OnDestroyPooledMob, _collectionCheck,_mobPoolMaxSize);
    }
    private void Start()
    {
        SetSOsSettings();
        SpawnHeroOnCamp();
    }
    private void Update()
    {
        SpawnObjectsOnMap();
    }
    private void OnEnable()
    {
        //OnMobSpawn += AddClassAiMobs;
        //OnHeroSpawn += AddClassHeros;
    }
    private void OnDisable()
    {
        //OnMobSpawn -= AddClassAiMobs;
        //OnHeroSpawn -= AddClassHeros;
    }
    void SpawnObjectsOnMap()//this will stay as control
    {
        //GameObject spawningObject;
        if (totalSpawnedObjects < _arenaSetting.maxObjectsInArea)
        {
            int random = UnityEngine.Random.Range(0, 2);
            //Debug.Log(random);
            if (random == 0 && spawnedEnemyUnits < _arenaSetting.maxEnemyUnitsInArea)
            {
               _spawnedMobPool.Get();
                //CreateMobInstantiate(); //old way isntantiating deleting


                //spawn Unit
                //attempt to do it as event not good idea at all
            }
            else if (random == 1 && spawnedLootChests < _arenaSetting.maxLootChestsInArea)
            {
                SpawnLootChest();//will be get from pool of chests
                //spawn
            }
        }
        else return;
    }

    void CreateMobInstantiate()//this is our "instantiate" with all settings mob need
    {//will be discontinued when pooling is working
        SpawnObject(enemyUnit);
        RandomPositionForObject(spawnedGameObject);

        //add MOB specific classes
        //OnMobSpawn?.Invoke(spawnedGameObject);
        spawnedGameObject.AddComponent<UnitAiMobs>();
        spawnedGameObject.AddComponent<DropLoot>();
        //set unit race
        //SetUnitRace();
        spawnedGameObject.GetComponent<CharacterBuilder>().Head = _spawnedEnemyUnitRaceSO.race;
        spawnedGameObject.GetComponent<CharacterBuilder>().Ears = _spawnedEnemyUnitRaceSO.race;
        spawnedGameObject.GetComponent<CharacterBuilder>().Eyes = _spawnedEnemyUnitRaceSO.race;
        spawnedGameObject.GetComponent<CharacterBuilder>().Body = _spawnedEnemyUnitRaceSO.race;
        //set unit visuals (hair, ears
        //SetUnitVisuals();
        spawnedGameObject.GetComponent<CharacterBuilder>().Hair = "";
        //set unit class, gear, weapon
        //SetUnitClass();
        spawnedGameObject.GetComponent<CharacterBuilder>().Rebuild();//this will reload visual
        //set unit stats
        //SetUnitStats();
        spawnedGameObject.GetComponent<UnitStatsAndInfo>().unitSettings = spawnedEnemyUnitSO;
        spawnedGameObject.GetComponent<UnitStatsAndInfo>().SetStats();
        //SetMobTypeTag();
        //set mob type and tag
        spawnedGameObject.GetComponent<ObjectInfo>().SetType("EnemyUnit");
        spawnedGameObject.gameObject.tag = "EnemyUnit";
        spawnedGameObject.GetComponent<UnitTargetPicker>().tagOfEnemy = "HeroUnit";
        //for counting right ammount in arena
        MobSpawned();
    }
    #region MobPooling
    private GameObject CreateMob()//this is our "instantiate" with all settings mob need
    {
        GameObject spawnedMob = Instantiate(enemyUnit);
        RandomPositionForObject(spawnedMob);

        //add MOB specific classes
        //OnMobSpawn?.Invoke(spawnedGameObject);
        spawnedMob.AddComponent<UnitAiMobs>();
        spawnedMob.AddComponent<DropLoot>();
        //set unit race
        //SetUnitRace();
        spawnedMob.GetComponent<CharacterBuilder>().Head = _spawnedEnemyUnitRaceSO.race;
        spawnedMob.GetComponent<CharacterBuilder>().Ears = _spawnedEnemyUnitRaceSO.race;
        spawnedMob.GetComponent<CharacterBuilder>().Eyes = _spawnedEnemyUnitRaceSO.race;
        spawnedMob.GetComponent<CharacterBuilder>().Body = _spawnedEnemyUnitRaceSO.race;
        //set unit visuals (hair, ears
        //SetUnitVisuals();
        spawnedMob.GetComponent<CharacterBuilder>().Hair = "";
        //set unit class, gear, weapon
        //SetUnitClass();
        spawnedMob.GetComponent<CharacterBuilder>().Rebuild();//this will reload visual
        //set unit stats
        //SetUnitStats();
        spawnedMob.GetComponent<UnitStatsAndInfo>().unitSettings = spawnedEnemyUnitSO;
        spawnedMob.GetComponent<UnitStatsAndInfo>().SetStats();
        //SetMobTypeTag();
        //set mob type and tag
        spawnedMob.GetComponent<ObjectInfo>().SetType("EnemyUnit");
        spawnedMob.gameObject.tag = "EnemyUnit";
        spawnedMob.GetComponent<UnitTargetPicker>().tagOfEnemy = "HeroUnit";
        //for counting right ammount in arena
        MobSpawned();

        spawnedMob.GetComponent<UnitHealth>().objectPool = _spawnedMobPool;
        return spawnedMob;
    }
    
    void OnReleaseToMobPool(GameObject spawnedMob)
    {//return to pool
        spawnedMob.gameObject.SetActive(false);

    }
    void OnGetFromMobPool(GameObject spawnedMob)
    {//get from pool
        spawnedMob.gameObject.SetActive(true);
        RandomPositionForObject(spawnedMob);
        spawnedMob.GetComponent<UnitHealth>().Respawn();

    }
    void OnDestroyPooledMob(GameObject spawnedMob)
    {//destroy if over capcity of pool
        Destroy(spawnedMob.gameObject);
    }
    #endregion

    void SpawnObject(GameObject gameObject)
    {
        spawnedGameObject = Instantiate(gameObject);
    }

    void RandomPositionForObject(GameObject gameObject)
    {
        //spawnedGameObject = Instantiate(gameObject);

        //this will set parent object
        gameObject.transform.parent = combatAreaSpawn.transform;

        //random place in Map relative to parent object (centre)
        float vectorX = (UnityEngine.Random.Range(-maxXaxisOfArea, maxXaxisOfArea) + combatAreaSpawn.transform.position.x);
        float vectorY = (UnityEngine.Random.Range(-maxYaxisOfArea, maxYaxisOfArea) + combatAreaSpawn.transform.position.y);
        float vectorZ = 0f;

        Vector3 spawnPointVector = new Vector3(vectorX, vectorY, vectorZ);
        //selfQuaternion  needed for vector3
        //Quaternion rotation = Quaternion.identity;

        //place where we want him
        gameObject.transform.position = spawnPointVector;
        //Debug.Log("shouldHaveSpawn " + vectorX + " " + vectorY);

        //float realX = spawnedGameObject.transform.position.x;
        //float realY = spawnedGameObject.transform.position.y;
        //Debug.Log("spawnedin "+realX +" " +realY );

        // set name and type
    }

    //*************************************** methods that will subscribe to events
    
    void SpawnLootChest()
    {
        SpawnObject(lootChest);
        RandomPositionForObject(spawnedGameObject);
        AddLootChestCounters();
    }
    public void MobSpawned()
    {
        spawnedEnemyUnits++;
        totalSpawnedObjects++;
    }
    public void MobDied()
    {
        StartCoroutine(WaitForRespawn());
        //spawnedEnemyUnits--;
        //totalSpawnedObjects--;
    }
    IEnumerator WaitForRespawn()
    { 
        yield return new WaitForSeconds(5f);
        spawnedEnemyUnits--;
        totalSpawnedObjects--;
    }
    void AddLootChestCounters()
    {
        spawnedLootChests++;
        totalSpawnedObjects++;
    }
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

    #region SpawnHero
    //whole region will be removed eventually
    public void SpawnHeroOnCamp()
    {//here to test combat and stuff will be removed at some point
        //HeroSpawner Class/object wil ldo it
        spawnedHero = Instantiate(heroUnit);
        

        //add MOB specific classes
        //OnHeroSpawn?.Invoke(spawnedHero);

        spawnedHero.AddComponent<UnitAiHeros>();
        spawnedHero.AddComponent<BackPack>();
        spawnedHero.AddComponent<UnitEquipment>();

        spawnedHero.GetComponent<ObjectInfo>().SetType("HeroUnit");
        spawnedHero.gameObject.tag = "HeroUnit";
        spawnedHero.GetComponent<UnitTargetPicker>().tagOfEnemy = "EnemyUnit";
        spawnedHero.transform.parent = combatAreaSpawn.transform; 
        spawnedHero.transform.position = heroCamp.transform.position;
        //set hero stats
        //SetHeroStats();
        spawnedHero.GetComponent<UnitStatsAndInfo>().unitSettings = spawnedHeroSO;
        spawnedHero.GetComponent<UnitStatsAndInfo>().SetStats();

        spawnedHero.GetComponent<UnitAiHeros>().task = Task.ADVENTURING;
    }
    #endregion
}
    #region Encapsulated methods
    //
    /*
    void AddClassAiMobs(GameObject gameObject)
    { 
        gameObject.AddComponent<UnitAiMobs>();
        gameObject.AddComponent<DropLoot>();
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

    void SetMobTypeTag()
    {
        spawnedGameObject.GetComponent<ObjectInfo>().SetType("EnemyUnit");
        spawnedGameObject.gameObject.tag = "EnemyUnit";
        spawnedGameObject.GetComponent<UnitTargetPicker>().tagOfEnemy = "HeroUnit";

    }//
    */

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
    /*
    void AddClassHeros(GameObject gameObject)
    {
        gameObject.AddComponent<UnitAiHeros>();
        gameObject.AddComponent<BackPack>();
        gameObject.AddComponent<UnitEquipment>();
    }
    void SetHeroStats()
    {
        //temp method for testing will be removed
        spawnedHero.GetComponent<UnitStatsAndInfo>().unitSettings = spawnedHeroSO;
        spawnedHero.GetComponent<UnitStatsAndInfo>().SetStats();
    }
    */
   
    #endregion
    /*void SpawnMeSomeUnits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnUnitRandomly();
        }
    }*/


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
