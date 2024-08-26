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

        //SpawnMeSomeUnits(4);
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
    void SpawnHeroOnCamp()
    {
            spawnedHero = Instantiate(heroUnit);
            spawnedHero.transform.parent = combatAreaSpawn.transform;
            spawnedHero.transform.position = heroCamp.transform.position;
    }
    /*void SpawnMeSomeUnits(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnUnitRandomly();
        }
    }*/
    void SpawnObjectsOnMap()
    {
        //GameObject spawningObject;
        if (totalSpawnedObjects < arenaSetting.maxObjectsInArea)
        {
            int random = Random.Range(0,2);
            //Debug.Log(random);
            if (random == 0 && spawnedEnemyUnits < arenaSetting.maxEnemyUnitsInArea)
            {
                SpawnObjectRandomly(enemyUnit);
            }
            else if (random == 1 && spawnedLootChests < arenaSetting.maxLootChestsInArea)
            {
                SpawnObjectRandomly(lootChest);
            }
        }
        else return;
    }

    void SpawnObjectRandomly(GameObject gameObject)
    {
        spawnedGameObject = Instantiate(gameObject);

        //this will set parent object
        spawnedGameObject.transform.parent = combatAreaSpawn.transform;

        //random place in Map relative to parent object
        float vectorX = (Random.Range(-maxXaxisOfArea, maxXaxisOfArea) + combatAreaSpawn.transform.position.x);
        float vectorY = (Random.Range(-maxYaxisOfArea, maxYaxisOfArea) + combatAreaSpawn.transform.position.y);
        float vectorZ = 0f;

        Vector3 spawnPointVector = new Vector3(vectorX, vectorY, vectorZ);
        //selfQuaternion  needed for vector3
        Quaternion rotation = Quaternion.identity;

        //place where we want him
        spawnedGameObject.transform.position = spawnPointVector;
        //Debug.Log("shouldHaveSpawn " + vectorX + " " + vectorY);

        float realX = spawnedGameObject.transform.position.x;
        float realY = spawnedGameObject.transform.position.y;
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
    }
    
    void SpawnUnitRandomly()
    {
        spawnedUnit = Instantiate(enemyUnit);
        
        //this will set parent object
        spawnedUnit.transform.parent = combatAreaSpawn.transform;

        //random place in Map relative to parent object
        float vectorX= (Random.Range(-maxXaxisOfArea, maxXaxisOfArea)+ combatAreaSpawn.transform.position.x);
        float vectorY= (Random.Range(-maxYaxisOfArea, maxYaxisOfArea)+ combatAreaSpawn.transform.position.y);
        float vectorZ = 0f;

        Vector3 spawnPointVector = new Vector3(vectorX,vectorY,vectorZ);
        
        Quaternion rotation=Quaternion.identity;
                
        //place relative to parent object

        spawnedUnit.transform.position = spawnPointVector;
        //Debug.Log("shouldHaveSpawn " + vectorX + " " + vectorY);

        float realX=spawnedUnit.transform.position.x;
        float realY=spawnedUnit.transform.position.y;
        //Debug.Log("spawnedin"+realX +" " +realY );
        //spawnedUnit=Instantiate(unit, spawnPoint.transform);
    }
    void AddSpawnedObjectToList()
    {
        spawnedGameObjectList.Add(spawnedUnit);
    }
    void SpawnLootCrate()
    {
        Instantiate(lootChest);
    }
}
