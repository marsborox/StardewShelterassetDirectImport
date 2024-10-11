using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    public List<Item> items = new List<Item>();


    private void Start()
    {

    }

    public void Add(Item item)
    {
        items.Add(item);
        //add weight
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        //remove weight
    }
}
