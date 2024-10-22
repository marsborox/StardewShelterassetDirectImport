using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroTravel : MonoBehaviour
{
    GameObject destination;
    GameObject nextHop;
    UnitMovement unitMovement;
    // Start is called before the first frame update

    //will subsci
    private void Awake()
    {
        unitMovement = GetComponent<UnitMovement>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TravelingToArea()
    {
        unitMovement.Move(nextHop);
        //make rangefinder class for method in range in combat***
        //go to portal
        //spend virtually time traveling
        //spawn in area -- switch to adventuring or whatever reason we had
    }
    void ReturningHome()
    { 
        //if reason fulfilled (temporarily we will say till inventory full - inventory should trigger event
        //
    }
}
