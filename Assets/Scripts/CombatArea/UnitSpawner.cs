using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{//script for testing can be discontinued eventually
    [SerializeField] GameObject Unit1;
    [SerializeField] GameObject Unit2;
    [SerializeField] Transform SpawnPoint1;
    [SerializeField] Transform SpawnPoint2;

    private void Awake()
    {

        
    }
    void Start()
    {
        SpawnUnits();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnUnits()
    {
        SpawnTeam1();
        SpawnTeam1();
        SpawnTeam1();
        SpawnTeam1();
        SpawnTeam1();
        SpawnTeam1();
        SpawnTeam1();
        SpawnTeam1();
        SpawnTeam1();
        SpawnTeam2();
    }

    void SpawnTeam1()
    {
        Instantiate(Unit1,SpawnPoint1.transform);
    }
    void SpawnTeam2()
    { 
        Instantiate (Unit2,SpawnPoint2.transform);
    }
}
