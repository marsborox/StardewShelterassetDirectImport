using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum slotType {HEAD,CHEST, }

[CreateAssetMenu(fileName = "new Armor", menuName = "Item/Armor")]
public class ArmorItem : Item
{
    [Header("Armor")]

    int random;

    void Start()
    { 
        isStackable = false;
    }


}
