using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBarCanvasFixer : MonoBehaviour
{

    public void FixFlip(float xAxis)
    {
        transform.localScale = new Vector2(xAxis, transform.localScale.y); 
    }
}
