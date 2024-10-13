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
    GameObject _tempItemSlot;

    GameObject _spawnedItemSlot;




    public List <Item> _backPack = new List<Item>();

    public List<GameObject> _freeList;
    public List<GameObject> _usedList;
    
    private int _poolSize;
    //private bool _expandable;

    private void Awake()
    {
        _mainUI = FindObjectOfType<MainUI>();
        _freeList = new List<GameObject> ();
        _usedList = new List<GameObject> ();
        

    }
    void Start()
    {
        _poolSize = 5;
        //_expandable = true;

        for (int i = 0; i < _poolSize; i++)
        {
            GenerateSlot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {//first clear mess from last time as we got stuff hanging on disable
        _backPack.Clear();

        foreach (GameObject obj in _usedList)
        {
            ReturnObject(obj);
        }

        if (_mainUI.activeUnit == null)
        {
            Debug.Log("No Hero selected");

        }
        else
        {
            Debug.Log("Hero Selected");
            AddItemsToLocalList();
            DisplaySlots();
        }
    }
    private void OnDisable()
    {


        //clear slots
    }

    void AddItemsToLocalList()
    {
        foreach (Item item in _mainUI.activeUnit.GetComponent<BackPack>().items)
        {
            _backPack.Add(item);
            //Debug.Log(item.name);
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

}
