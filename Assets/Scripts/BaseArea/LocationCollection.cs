using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationCollection : MonoBehaviour
{
    [SerializeField] GameObject mine;
    [SerializeField] GameObject lake;
    [SerializeField] GameObject field;
    [SerializeField] GameObject volcano;
    [SerializeField] GameObject forest;
    [SerializeField] GameObject mainBase;

    public static GameObject[] locationArray = new GameObject[5];
    


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
        Debug.Log("Location Array Created");
    }

    public GameObject GetMine()
    {
        return mine;
    }
    public GameObject GetLake() 
    {
        return lake;
    }
    public GameObject GetField() 
    {
        return field;
    }
    public GameObject GetVolcano()
    {
        return volcano;
    }
    public GameObject GetForest()
    {
        return forest;
    }
    public GameObject GetMainBase() 
    {
        return mainBase;
    }

}
