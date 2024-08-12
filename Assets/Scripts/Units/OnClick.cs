using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    Unit unit;
    ObjectInfo objectInfo;
    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();
        objectInfo = GetComponent<ObjectInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) 
        
        {
            objectInfo.TellInfo();
        }
    }


}
