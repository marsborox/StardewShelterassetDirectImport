using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class BackPackUI : MonoBehaviour
{
    // Start is called before the first frame update

    MainUI _mainUI;
    [SerializeField] GameObject _itemSlot;
    [SerializeField] BackPackItemSlot _itemSlot2;

    private int _poolSize;
    //private bool _expandable;
    
    private void Awake()
    {
        _mainUI = FindObjectOfType<MainUI>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        SpawnSlots();


    }
    private void OnDisable()
    {
        DestroySlots();
    }
    void SpawnSlots()
    {
        Item importedItem;
        int numberOfSlots = _mainUI.activeUnit.GetComponent<BackPack>().items.Count;
        Debug.Log(numberOfSlots);
        for (int i = 0; i< _mainUI.activeUnit.GetComponent<BackPack>().items.Count; i++)

        {
            BackPackItemSlot itemSlot = Instantiate(_itemSlot2);
            itemSlot.transform.parent = transform;

            
            //broken for some reason need some fix mess with image sprite stuff
            //itemSlot.GetComponent<Image>().sprite =_mainUI.activeUnit.GetComponent<BackPack>().items[i].itemIcon;
        }/*
        foreach (Item item in _mainUI.activeUnit.GetComponent<BackPack>().items)
        {
            BackPackItemSlot itemSlot =Instantiate(_itemSlot2);
            itemSlot.transform.parent = transform;
            
            itemSlot.image=item.GetComponent<Image>();
        }*/
    }
    void DestroySlots()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
}
