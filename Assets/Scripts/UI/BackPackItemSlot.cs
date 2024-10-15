using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;



public class BackPackItemSlot : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Sprite sprite;
    [SerializeField] public TextMeshProUGUI count;

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
    public void SetSprite(Sprite importedSprite)
    {
        sprite = importedSprite;
    }
}

