using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Unity.VisualScripting;

using UnityEngine;

public enum TravelSteps {NOT,  }
public class HeroTravel : MonoBehaviour
{
    
    

    public GameObject destination;
    GameObject _nextHop;
    public GameObject basePortal;
    public GameObject travelLimbo;
    public GameObject baseArea;

    UnitMovement unitMovement;
    UnitAiBase unitAiBase;
   
    public float travellingTime;
    float touchDistance;
    // Start is called before the first frame update

    //will subsci
    private void Awake()
    {
        unitMovement = GetComponent<UnitMovement>();
        unitAiBase = GetComponent<UnitAiBase>();
    }
    void Start()
    {
        travellingTime = 5f;
        touchDistance = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TravelingToArea(GameObject destination)
    {
        
    }
    void TravelingToArea()
    { 
        
    }
    void ReturningHome()
    {
        GameObject camp = gameObject.GetComponentInParent<Camp>().gameObject;
        unitMovement.Move(camp);
        
        if (Vector2.Distance(this.transform.position, camp.transform.position) < touchDistance)//we have reached portal
        {
            gameObject.transform.parent = travelLimbo.transform;
        }
       
        //if reason fulfilled (temporarily we will say till inventory full - inventory should trigger event
        //
    }

    void LeavingArea(GameObject portaltoLeave)
    {//rework to on trigger enter
        if (Vector2.Distance(this.transform.position, portaltoLeave.transform.position) < touchDistance)//we have reached portal
        {
            gameObject.transform.parent = travelLimbo.transform;
        }
    }
    //make rangefinder class for method in range in combat***


    void GoToArea()
    {
        //StartCoroutine(TraveltoAreaRoutine(FromLimboToArea));
    }
    void GoHome()
    { }

    IEnumerable TraveltoAreaRoutine(Action method)
    {
        yield return new WaitForSeconds(travellingTime);
        method();
    }
    void FromLimboToArea()
    {
        gameObject.transform.parent = destination.GetComponent<CombatAreaSpawn>().gameObject.transform;
        gameObject.transform.position = destination.GetComponent<Camp>().transform.position;
        
    }
    void FromLimboToBase()
    {
        gameObject.transform.parent = basePortal.transform;
        gameObject.transform.position = baseArea.transform.position;
        
    }
    void TestingStuff(Action method)
    {
        method();
    }
    void RunStuff()
    { 
        TestingStuff(FromLimboToBase);
    }

}
