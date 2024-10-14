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
    [SerializeField] BackPackItemSlot _itemSlot2;//will rename eventually all if will work
    GameObject _tempItemSlot;//*

    GameObject _spawnedItemSlot;//*



    public List <Item> _backPack = new List<Item>();//*

    public List<GameObject> _freeList;//*
    public List<GameObject> _usedList;//*
    
    private IObjectPool<BackPackItemSlot> objectPool; 
    private List<BackPackItemSlot> _itemSlotsActive;

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int _itemSlotCapacity=5;
    [SerializeField] private int _maxItemSlotCapacity=1000;
    private int _poolSize;
    //private bool _expandable;
    
    private void Awake()
    {
        _mainUI = FindObjectOfType<MainUI>();

        objectPool = new ObjectPool<BackPackItemSlot>(CreateItemSlotInPool, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, _itemSlotCapacity, _maxItemSlotCapacity);

        _itemSlotsActive = new List<BackPackItemSlot>();
        _freeList = new List<GameObject> ();//*
        _usedList = new List<GameObject> ();//*
        

    }
    void Start()
    {/*
        _poolSize = 5;
        //_expandable = true;

        
        for (int i = 0; i < _poolSize; i++)//*
        {
            GenerateSlot();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        //Enabled();
        EnabledSecond();
    }
    private void OnDisable()
    {

    }
    #region Pooling from Unity Book
    void EnabledSecond()//will rename this
    {
        //clear UI backpack list

        _backPack.Clear();
        //return all slots into pool

        for (int i = 0; i < transform.childCount; i++)
        {
            objectPool.Release(this.transform.GetChild(i).GetComponent<BackPackItemSlot>());
        }
        _itemSlotsActive.Clear();

        //fill UI list with backpack items
        AddItemsToLocalList();

        //spawn slots
        for (int i =0; i<_backPack.Count; i++)
        {
            if (objectPool.CountInactive == 0)
            {
                BackPackItemSlot itemSlotInstance = CreateItemSlotInPool();
                itemSlotInstance.transform.parent = transform;
                OnGetFromPool(_itemSlot2);
            }
            else
            {
                OnGetFromPool(_itemSlot2);
            }
        }
        //fill slots with item data

    }
    void AddItemsToLocalList()
    {
        foreach (Item item in _mainUI.activeUnit.GetComponent<BackPack>().items)
        {
            _backPack.Add(item);
            //Debug.Log(item.name);
        }
    }


    // invoked when creating an item to populate the object pool
    private BackPackItemSlot CreateItemSlotInPool()
    {
        BackPackItemSlot itemSlotInstance = Instantiate(_itemSlot2);
        itemSlotInstance.ObjectPool = objectPool;
        return itemSlotInstance;
    }
    // invoked when returning an item to the object pool
    private void OnReleaseToPool(BackPackItemSlot itemSlot)
    { 
        itemSlot.gameObject.SetActive(false);
    }
    // invoked when retrieving the next item from the object pool
    private void OnGetFromPool(BackPackItemSlot itemSlot)
    {
        itemSlot.gameObject.SetActive(true);
        _itemSlotsActive.Add(itemSlot);
    }
    // invoked when we exceed the maximum number of pooled items (i.e.destroy the pooled object)

    private void OnDestroyPooledObject(BackPackItemSlot itemslot)
    {
        Destroy(itemslot.gameObject);
    }

    #endregion
    #region SanyanPooling not working tho
    void Enabled()//*
    {
        //clear slots
        
        
        
        /*if (!(_usedList.Count == 0))
        {
            for (int i = _usedList.Count - 1; i < 0; i--)
            {
                ReturnObject(_usedList[i]);
            }
        }*/
        
        foreach (GameObject obj in _usedList)
        {
            ReturnObject(obj);
        }

        if (_mainUI.activeUnit == null)
        {
            Debug.Log("No Unit selected");

        }
        else
        {//fill slots
            Debug.Log("Hero Selected");
            _backPack.Clear();
            AddItemsToLocalList();
            DisplaySlots();
        }
    }

    void DisplaySlots()
    {
        for (int i=0; i<_backPack.Count;i++)
        {
            _tempItemSlot = GetSlot();
            _tempItemSlot.SetActive(true);
        }
    }
    void GenerateSlot()
    {
        GameObject itemSlot = Instantiate(_itemSlot);
        itemSlot.transform.parent = transform;
        itemSlot.SetActive(false);

        _freeList.Add(itemSlot);
    }
    void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _usedList.Remove(obj);
        _freeList.Add(obj);
    }
    public GameObject GetSlot()
    {
        int totalFree = _freeList.Count;

        if (_freeList.Count == 0)
        {
            GenerateSlot();
            //GetSlot();
            totalFree = _freeList.Count;
        }

        GameObject g = _freeList[totalFree-1]; //here was -1

        _freeList.RemoveAt(totalFree-1);//here was but added by me -1
        _usedList.Add(g);
        return g;
    }
    #endregion
}
