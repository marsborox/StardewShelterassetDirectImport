using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Race", fileName = "RaceProperties")]
public class UnitRaceSO : ScriptableObject
{

    [SerializeField] public string race;
    [SerializeField] public string skinColorHex;
    [SerializeField] public EnemyType enemyType;
    

    // desired format
    //ZombieB#F6CA9F/0:0:0
    //Human#5AC54F/0:0:0
    //Human#FFFFFF/0:0:0

    //eventually we will have string builder for race variants 


    /* //This will be done automaticly same for all body parts in 
       // ObjectSpawner of area and CharacterBuilder on prefab
    public string Head;
    public string Ears;
    public string Eyes;
    public string Body;
    */
    // public string Hair; will be in visuals prob.

}
