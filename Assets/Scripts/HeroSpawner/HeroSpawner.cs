using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ObjectSpawner;
using static ObjectSpawnerPrePoolingBackup;

public class HeroSpawner : MonoBehaviour
{//this class is for spawning hero and cheating stuff on him for testing purposes

    [SerializeField] GameObject _heroUnit;
    [SerializeField] GameObject _heroCamp;
    [SerializeField] UnitSO _spawnedHeroSO;

    [SerializeField] GameObject _basePortal;
    [SerializeField] GameObject _baseArea;
    [SerializeField] GameObject _travelLimbo;
    

    GameObject spawnedHero;
    // Start is called before the first frame update

    public delegate void SpawnEvent(GameObject gameObject);

    public event SpawnEvent OnHeroSpawn;

    public GameObject arenas;
    //delegate in objectSpawner
    
    private void Awake()
    {
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


    public void SpawnHeroOnCamp()
    {//temp method will remove later
        

        spawnedHero = Instantiate(_heroUnit);

        OnHeroSpawn?.Invoke(spawnedHero);
        //add hero classes
        spawnedHero.AddComponent<UnitAiHeros>();
        spawnedHero.AddComponent<BackPack>();
        spawnedHero.AddComponent<UnitEquipment>();
        spawnedHero.AddComponent<HeroTravel>();


        spawnedHero.GetComponent<HeroTravel>().basePortal=_basePortal;
        spawnedHero.GetComponent<HeroTravel>().travelLimbo=_travelLimbo;
        spawnedHero.GetComponent<HeroTravel>().baseArea=_baseArea;

        spawnedHero.GetComponent<ObjectInfo>().SetType("HeroUnit");
        spawnedHero.gameObject.tag = "HeroUnit";
        spawnedHero.GetComponent<UnitTargetPicker>().tagOfEnemy = "EnemyUnit";
        spawnedHero.transform.parent = _baseArea.transform;
        spawnedHero.transform.position = _heroCamp.transform.position;
        SetHeroStats();
        spawnedHero.GetComponent<UnitAiHeros>().task = Task.IDLE;

        //set hero stats - is temp for testing
        spawnedHero.GetComponent<UnitStatsAndInfo>().unitSettings = _spawnedHeroSO;
        spawnedHero.GetComponent<UnitStatsAndInfo>().SetStats();
    }

    void AddClassHeros(GameObject gameObject)
    {
        gameObject.AddComponent<UnitAiHeros>();
        gameObject.AddComponent<BackPack>();
        gameObject.AddComponent<UnitEquipment>();
    }
    void SetHeroStats()
    {
        //temp method for testing will be removed
        spawnedHero.GetComponent<UnitStatsAndInfo>().unitSettings = _spawnedHeroSO;
        spawnedHero.GetComponent<UnitStatsAndInfo>().SetStats();
    }
}
