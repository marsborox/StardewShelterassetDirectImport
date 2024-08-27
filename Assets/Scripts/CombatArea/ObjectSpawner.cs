using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Max y and x axis of area")]
    [SerializeField] float maxXaxisOfArea;
    [SerializeField] float maxYaxisOfArea;
    // x-11,11; y-8,8 
    [SerializeField] GameObject enemyUnit;
    [SerializeField] GameObject lootChest;
    [SerializeField] GameObject heroUnit;
    [SerializeField] GameObject heroCamp;

    public ArenaSettingSO arenaSetting;

    //[SerializeField] int maxObjectsInArea = 10;
    //[SerializeField] int maxLootChestsInArea = 6;
    //[SerializeField] int maxEnemyUnitsInArea = 8;



    [SerializeField] public int spawnedEnemyUnits;
    [SerializeField] public int spawnedLootChests;
    [SerializeField] public int totalSpawnedObjects;

    GameObject spawnedUnit;
    GameObject spawnedGameObject;
    GameObject spawnedHero;

    //ObjectInfo objectInfo;
    //this is name of parent object of arena
    [SerializeField] public GameObject combatAreaSpawn;

    static List<GameObject> spawnedGameObjectList = new List<GameObject>();

    int nameCounter = 0;

    private void Awake()
    {
        //objectInfo = FindObjectOfType<ObjectInfo>();
    }
    private void Start()
    {
        SpawnHeroOnCamp();


    }
    private void Update()
    {
        SpawnObjectsOnMap();

    }
    public void UnitDied()
    {
        spawnedEnemyUnits--;
        totalSpawnedObjects--;
    }

    void SpawnHeroOnCamp()
    {
        spawnedHero = Instantiate(heroUnit);
        spawnedHero.transform.parent = combatAreaSpawn.transform;
        spawnedHero.transform.position = heroCamp.transform.position;
    }

    void SpawnObjectsOnMap()
    {
        //GameObject spawningObject;
        if (totalSpawnedObjects < arenaSetting.maxObjectsInArea)
        {
            int random = Random.Range(0, 2);
            //Debug.Log(random);
            if (random == 0 && spawnedEnemyUnits < arenaSetting.maxEnemyUnitsInArea)
            {
                //SpawnObjectRandomlyOrig(enemyUnit);
                SpawnEnemy();
                //spawn Unit
            }
            else if (random == 1 && spawnedLootChests < arenaSetting.maxLootChestsInArea)
            {
                //SpawnObjectRandomlyOrig(lootChest);
                SpawnLootChest();
                //spawn
            }
        }
        else return;
    }
    void SpawnEnemy()
    {
        SpawnObjectRandomly(enemyUnit);
        SetNameAndCounters();

    }
    void SpawnLootChest()
    {
        SpawnObjectRandomly(lootChest);
        SetNameAndCounters();

    }

    void SpawnObjectRandomly(GameObject gameObject)
    {
        spawnedGameObject = Instantiate(gameObject);

        //this will set parent object
        spawnedGameObject.transform.parent = combatAreaSpawn.transform;

        //random place in Map relative to parent object (centre)
        float vectorX = (Random.Range(-maxXaxisOfArea, maxXaxisOfArea) + combatAreaSpawn.transform.position.x);
        float vectorY = (Random.Range(-maxYaxisOfArea, maxYaxisOfArea) + combatAreaSpawn.transform.position.y);
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

    void SetNameAndCounters()
    {
        string name;
        name = spawnedGameObject.GetComponent<ObjectInfo>().name + nameCounter;
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
        else if (type == "LootChest")
        {
            spawnedLootChests++;
            totalSpawnedObjects++;
        }

        //spawnedGameObjectList.Add(gameObject.name,gameObject.GetType});
        //add to list
    }
    void SpawnObjectRandomlyOrig(GameObject gameObject)
    {
        spawnedGameObject = Instantiate(gameObject);

        //this will set parent object
        spawnedGameObject.transform.parent = combatAreaSpawn.transform;

        //random place in Map relative to parent object (centre)
        float vectorX = (Random.Range(-maxXaxisOfArea, maxXaxisOfArea) + combatAreaSpawn.transform.position.x);
        float vectorY = (Random.Range(-maxYaxisOfArea, maxYaxisOfArea) + combatAreaSpawn.transform.position.y);
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
        Debug.Log(name);
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
    }

    void SetUnitRace()
    { 
        
    }
    void SetUnitClass()
    { 
    
    }

    void AddSpawnedObjectToList()
    {
        spawnedGameObjectList.Add(spawnedUnit);
    }
    void SpawnLootCrate()
    {
        Instantiate(lootChest);
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
            x = Random.Range(-maxXaxisOfArea, maxXaxisOfArea);
            y = Random.Range(-maxYaxisOfArea, maxYaxisOfArea);
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
