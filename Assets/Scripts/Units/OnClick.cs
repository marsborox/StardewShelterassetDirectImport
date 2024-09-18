using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    Unit unit;
    ObjectInfo objectInfo;
    MainUI mainUI;

    void Start()
    {
        unit = GetComponent<Unit>();
        objectInfo = GetComponent<ObjectInfo>();
        mainUI = FindObjectOfType<MainUI>();
    }

    
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) 
        
        {
            mainUI.activeUnit=this.gameObject;
            mainUI.RefreshUI();
            //working now lets make UI to display
            //objectInfo.TellInfo();
            Debug.Log("clicked on something");
        }
    }


}
