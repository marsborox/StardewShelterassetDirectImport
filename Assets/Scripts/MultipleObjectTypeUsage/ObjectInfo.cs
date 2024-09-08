using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    //this is only for area spawning
    [SerializeField] public string name;
    [SerializeField] public string type;

    void Start()
    { 
        
    }
    public void SetName(string newName)
    {
        name= newName;
    }
    public void SetType(string newType)
    {
        type= newType; 
    }
    public string GetName()
    {
        return name;
    }
    public string GetType()
    {
        return type; 
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
    public void TellInfo()
    {
        Debug.Log("telling info");
        //Debug.Log("my name is" + name);
        
    }
}
