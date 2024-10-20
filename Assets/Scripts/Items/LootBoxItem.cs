using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Lootbox", menuName = "Item/LootBox")]
public class LootBoxItem : Item
{
    public void Open()
    {
        Debug.Log("LootBox Opened");
    }
}
