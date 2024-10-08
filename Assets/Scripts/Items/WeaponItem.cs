using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum weaponSlotType { ONEHAND, TWOHAND }
[CreateAssetMenu(fileName = "new Weapon", menuName = "Item/Weapon")]
public class WeaponItem : Item
{
    [Header("Weapon")]

    public string slotType;
    public int damage;
    public float attackSpeed;
}
