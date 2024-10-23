using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TravelSteps {NOT,  }
public class HeroTravel : MonoBehaviour
{
    public GameObject destination;
    GameObject _nextHop;
    public GameObject portal;
    public GameObject travelLimbo;
    UnitMovement unitMovement;

    public float travellingTime;
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

    void TravelingToArea(GameObject destination)
    {
        //unitMovement.Move(_nextHop);
        unitMovement.Move(portal);

        if (Vector2.Distance(this.transform.position, portal.transform.position) < 1)//we have reached portal
        {
            gameObject.transform.parent = travelLimbo.transform;
        }

        //spend virtually time traveling
        //spawn in area -- switch to adventuring or whatever reason we had
    }
    void ReturningHome()
    {
        GameObject camp = gameObject.GetComponentInParent<Camp>().gameObject;
        unitMovement.Move(camp);
        
        if (Vector2.Distance(this.transform.position, camp.transform.position) < 1)//we have reached portal
        {
            gameObject.transform.parent = travelLimbo.transform;
        }
        //if reason fulfilled (temporarily we will say till inventory full - inventory should trigger event
        //
    }
    void LeavingArea(GameObject portaltoLeave)
    {//rework to on trigger enter
        if (Vector2.Distance(this.transform.position, portaltoLeave.transform.position) < 1)//we have reached portal
        {
            gameObject.transform.parent = travelLimbo.transform;
        }
    }
    //make rangefinder class for method in range in combat***


    void FromLimboToArea()
    {
        gameObject.transform.parent = destination.GetComponent<CombatAreaSpawn>().gameObject.transform;
        gameObject.transform.position = destination.GetComponent<Camp>().transform.position;
    }
    IEnumerable TravelRoutine()
    {
        yield return new WaitForSeconds(travellingTime);
        FromLimboToArea();
    }

}
