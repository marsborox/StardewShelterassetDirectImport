using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Item : ScriptableObject
{
    [Header("Item")]

    public string itemName;
    public Sprite itemIcon;
    public int weight;
    public bool isStackable;//shoudl be somehow overriden in child class, fck it we do it in awakes
}
