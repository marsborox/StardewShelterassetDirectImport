using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit", fileName = "UnitProperties")]
public class UnitSO : ScriptableObject
{
    [SerializeField] public int range;
    [SerializeField] public int damage;
    [SerializeField] public float attackSpeed;
    [SerializeField] public float movementSpeed;
    [SerializeField] public int health;

    /*
    [SerializeField] public string race;


    public string Head;
    public string Ears;
    public string Eyes;
    public string Body;
    public string Hair;
    */

    public string Armor;
    public string Helmet;
    public string Weapon;
    public string Firearm;
    public string Shield;
    public string Cape;
    public string Back;
    public string Mask;
    public string Horns;
}
