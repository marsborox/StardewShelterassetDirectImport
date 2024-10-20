using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField] Item _lootBox;
    [SerializeField] Item _sword;


    Item _itemToAdd;


    MainUI mainUI;
    // Start is called before the first frame update

    private void Awake()
    {
        mainUI = FindObjectOfType<MainUI>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CraftSwordToClickedHero()
    {
        if (mainUI.activeUnit != null)
        {
            CraftItem(mainUI.activeUnit, _sword);
        }
    }
    public void CraftBoxToClickedHero()
    {
        if (mainUI.activeUnit != null)
        {
            CraftItem(mainUI.activeUnit, _lootBox);
        }
    }

    public void AddItemToActiveHero()
    {
        if (mainUI.activeUnit != null)
        {
            mainUI.activeUnit.GetComponent<BackPack>().items.Add(_itemToAdd);
        }
    }
    public void CraftItem(GameObject hero, Item item)
    {
        hero.GetComponent<BackPack>().items.Add(item);
    }
}
