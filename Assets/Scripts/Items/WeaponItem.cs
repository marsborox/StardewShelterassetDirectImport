using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "new Weapon", menuName = "Item/Weapon")]
public class WeaponItem : Item
{
    [Header("Weapon")]

    public string slotType;
    public int damage;
    public float attackSpeed;

    void Start()
    {
        isStackable = false;
    }

    public virtual void EquipItem()
    { 
        
    }
}
