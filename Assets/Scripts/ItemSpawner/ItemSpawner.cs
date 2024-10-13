using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField] Item  itemToAdd;

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

    public void AddItemToActiveHero()
    {
        if (mainUI.activeUnit != null)
        {
            mainUI.activeUnit.GetComponent<BackPack>().items.Add(itemToAdd);
        }
    }
}
