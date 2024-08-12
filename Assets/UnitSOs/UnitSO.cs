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
}
