using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCollection : MonoBehaviour
{
    [SerializeField] Transform mine;
    [SerializeField] Transform lake;
    [SerializeField] Transform field;
    [SerializeField] Transform volcano;
    [SerializeField] Transform forest;
    [SerializeField] Transform mainBase;

    public static Transform[] locationArray = new Transform[5];
    


    // Start is called before the first frame update
    void Start()
    {
        CreateArrayOfLocations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateArrayOfLocations()
    {
        
        locationArray[0] = mine;
        locationArray[1] = lake;
        locationArray[2] = field;
        locationArray[3] = volcano;
        locationArray[4] = forest;
    }

    public Transform GetMine()
    {
        return mine;
    }
    public Transform GetLake() 
    {
        return lake;
    }
    public Transform GetField() 
    {
        return field;
    }
    public Transform GetVolcano()
    {
        return volcano;
    }
    public Transform GetForest()
    {
        return forest;
    }
    public Transform GetMainBase() 
    {
        return mainBase;
    }

}
