using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static ObjectSpawner;

public class HeroManager : MonoBehaviour
{
    public List<GameObject> heroList = new List<GameObject>();


    public HeroSpawner heroSpawner;

    private void Awake()
    {
        heroSpawner = FindObjectOfType<HeroSpawner>();
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
        heroSpawner.OnHeroSpawn += AddHeroToList;
    }

    private void OnDisable()
    {
        //OnMobSpawn -= AddClassAiMobs;
        heroSpawner.OnHeroSpawn -= AddHeroToList;
    }

    public void AddHeroToList(GameObject gameObject)
    { 
        heroList.Add (gameObject);
    }
}
