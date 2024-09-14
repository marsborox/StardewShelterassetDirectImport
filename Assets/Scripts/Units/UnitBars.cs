using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.UI;

public class UnitBars : MonoBehaviour
{
    [SerializeField] public Image healthBarSprite;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void FixFlip(float xAxis)
    {
        transform.localScale = new Vector2(xAxis, transform.localScale.y);
    }
}
