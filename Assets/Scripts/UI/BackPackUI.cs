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
    
    [SerializeField] BackPackItemSlot _itemSlot;
    int _spawnedSlots;
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
        SpawnSlots();
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        _spawnedSlots = 0;
        //DestroySlots();
    }
    void SpawnSlots()
    {
        
        int numberOfSlotsToSpawn = _mainUI.activeUnit.GetComponent<BackPack>().items.Count;
        if (!(_spawnedSlots == numberOfSlotsToSpawn))
        {
            DestroySlots();
            _spawnedSlots = numberOfSlotsToSpawn;
            Debug.Log("Change Detected");
            Debug.Log(numberOfSlotsToSpawn);
            for (int i = 0; i < _mainUI.activeUnit.GetComponent<BackPack>().items.Count; i++)

            {
                BackPackItemSlot itemSlot = Instantiate(_itemSlot);
                //Display IMG first is from tutorial but i like second one more for...reasons
                //itemSlot.transform.GetChild(0).GetComponent<Image>().sprite = _mainUI.activeUnit.GetComponent<BackPack>().items[i].itemIcon;
                itemSlot.GetComponent<BackPackItemSlot>().image.sprite = _mainUI.activeUnit.GetComponent<BackPack>().items[i].itemIcon;

                itemSlot.transform.parent = transform;
            }
        }
    }
    void DestroySlots()
    {

        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
}
