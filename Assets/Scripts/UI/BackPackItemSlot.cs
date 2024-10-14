using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;


public class BackPackItemSlot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _count;
    public IObjectPool<BackPackItemSlot> objectPool;

    public IObjectPool<BackPackItemSlot> ObjectPool { set => objectPool = value; }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    void Deactivate()
    {
        objectPool.Release(this);
    }
}

