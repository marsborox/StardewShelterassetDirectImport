using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item : ScriptableObject
{
    [Header("Item")]

    public string itemName;
    public Sprite itemIcon;
    public int weight;
}
