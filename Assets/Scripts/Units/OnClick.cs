using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    Unit unit;
    ObjectInfo objectInfo;
    
    void Start()
    {
        unit = GetComponent<Unit>();
        objectInfo = GetComponent<ObjectInfo>();
    }

    
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) 
        
        {
            //working now lets make UI to display
            objectInfo.TellInfo();
        }
    }


}
