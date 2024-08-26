using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Race", fileName = "RaceProperties")]
public class UnitRaceSOs : ScriptableObject
{

    [SerializeField] public string race;
    [SerializeField] public string skinColorHex;

    // desired format
    //ZombieB#F6CA9F/0:0:0
    //Human#5AC54F/0:0:0
    //Human#FFFFFF/0:0:0

    public string Head;
    public string Ears;
    public string Eyes;
    public string Body;
    public string Hair;
}
