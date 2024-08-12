using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{

    [SerializeField] public string name;
    [SerializeField] public string type;




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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TellInfo()
    {
        Debug.Log("my name is" + name);
    }
}
