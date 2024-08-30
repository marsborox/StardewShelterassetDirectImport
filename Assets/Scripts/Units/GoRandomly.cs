using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
// this is just a script for random movement - testing purposes, will get removed at some point
//DISCONTINUED AND BROKEN!!!!!!!!!!!!!!!


public class GoRandomly : MonoBehaviour
{
    //set ai true and go randomly true to make this work
    [SerializeField] public bool isAi;
    [SerializeField] bool doMining;
    [SerializeField] public bool goRandomly;
    [SerializeField] float waitTime = 2f;
    [SerializeField] bool findTarget;


    LocationCollection locationCollection;

    UnitMovement unitMovement;
    GameObject _target;
    public GameObject locationTarget;

    GameObject[] _locationArray;


    public Collider2D colliderOfWhatAreWeTouching;
    //public string targetString;

    public string targetLocation;
    public bool weAreWhereWeWantToBe;

    public bool weTouchedSomething = false;
    public bool hasArrived = false;
    public bool doWeHaveOrder = false;
    public bool pause;

    public string unitLocation;
    //must be value so we start with something to compare
    //public int oldTargetIndex;
    public int newTargetIndex;
    
    
    private void Awake()
    {
        //locationCollection = FindObjectOfType<LocationCollection>();
        unitMovement = GetComponent<UnitMovement>();//
        locationCollection = FindObjectOfType<LocationCollection>();//
    }
    void Start()
    {
        //state = Activity.IDLE;


        //CheckAllLocationLocal();
        PickRandomTarget();
        //oldTargetIndex = 0;
        pause = false;
        targetLocation = "None";
        unitLocation = "None";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GoRandomlyIfShouldAndNotPaused();
        ForceRandomTarget();
        /*
        if (_target == null)
        {
            findTarget = true;
        }
        */
    }

    void PerformMoveToTarget()
    {
        //SetLocationTransform();
        unitMovement.Move(_target);
    }

    void ThinkFlow()
    {

    }

    public void GoRandomlyIfShouldAndNotPaused()
    {
        if (_target != null)
        {
            if ((!pause) && goRandomly)
                PerformMoveToTarget();

            if (weTouchedSomething)
            {//we know where we are moment we touch it

                CheckIfHasArrived(unitLocation);
                weTouchedSomething = false;
                if (hasArrived)
                {
                    StartCoroutine(Wait());
                    PickRandomTarget();
                    hasArrived = false;
                }
            }
        }
        else 
        {
            Debug.Log("no target in GoRandomly");
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        weTouchedSomething = true;

        if (otherCollider.gameObject.tag == "Location")
        {
            unitLocation = otherCollider.gameObject.name;
            colliderOfWhatAreWeTouching = otherCollider;
        }
    }



    Collider2D ReturnColliderOfWhatWeAreTouching()
    {
        return colliderOfWhatAreWeTouching;
    }

    void SetTargetLocationMine()
    {
        _target = locationCollection.GetMine();
    }
    void SetTargetLocationMainBase()
    {
        _target = locationCollection.GetMainBase();
    }
    void DoMining()
    {
        Debug.Log("ImMining");
        for (int i = 0; i < 10; i++)
        {
            StartCoroutine(Wait());
            Debug.Log("Mining Finished");
        }
    }

    bool CheckIfHasArrived(string other)
    {
        if (other == locationTarget.name)
        {
            return hasArrived = true;
        }
        return hasArrived = false;
    }

    public void CheckAllLocation()
    {
        string locations = "Location";

        //locationArray = GameObject.FindGameObjectsWithTag(locations);
        _locationArray = LocationCollection.locationArray;
        //Debug.Log($"LocationCollection array lenght:" + LocationCollection.locationArray.Length);
        //Debug.Log($"GoRandomly array Length:" + _locationArray.Length);
    }
    public void PickRandomTarget()
    {
        int oldTargetIndex = newTargetIndex;
        CheckAllLocation();
        int i = _locationArray.Length;
        newTargetIndex = Random.Range(1, i--);
        //Debug.Log("ArrayLength "+ i);
        i = i++;
        //check if new location is same as old   
        while (newTargetIndex == oldTargetIndex)
        {
            //Debug.Log("Duplicity");
            newTargetIndex = Random.Range(0, i);
        }
        //oldTargetIndex = newTargetIndex;
        //Debug.Log("NextTargetIndex: "+ newTargetIndex);
        _target = _locationArray[newTargetIndex];
        locationTarget = _target;
        //Debug.Log("Location Name: " + locationTarget.name);
        targetLocation = locationTarget.name;
        hasArrived = false;


        //SetLocationTarget();
        //return target;
    }

    void SetLocationTarget()
    {
        _target = locationTarget.GetComponent<GameObject>();
    }
    void GetTransformOfTarget()
    {

    }

    bool CheckIfAi()
    {
        return isAi;
    }

    public void SetUnitLocationNone()
    {
        unitLocation = "None";
    }
    public void SetUnitActivity()
    {

    }
    IEnumerator Wait()
    {
        //Debug.Log("We are waiting");
        pause = true;
        yield return new WaitForSeconds(waitTime);
        pause = false;
        //something go
    }
    void ForceRandomTarget()
    {
        if (findTarget)
        { 
            PickRandomTarget();
            findTarget = false;
        }
    }
}
