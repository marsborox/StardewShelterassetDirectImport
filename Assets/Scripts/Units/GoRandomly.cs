using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
// this is jsut a script for random movement - testing purposes



public class GoRandomly : MonoBehaviour
{
    //set ai true and go randomly true to make this work
    [SerializeField] public bool isAi;
    [SerializeField] bool doMining;
    [SerializeField] public bool goRandomly;
    [SerializeField] float waitTime = 2f;
    LocationCollection locationCollection;

    UnitMovement unitMovement;
    Transform targetTransform;
    public GameObject locationTarget;

    public GameObject[] locationArray;


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

    public Activity state;
    private void Awake()
    {
        //locationCollection = FindObjectOfType<LocationCollection>();
        unitMovement = GetComponent<UnitMovement>();//
        locationCollection = FindObjectOfType<LocationCollection>();//
    }
    void Start()
    {
        state = Activity.IDLE;


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
    }

    void PerformMoveToTarget()
    {
        SetLocationTransform();
        unitMovement.Move(targetTransform);
    }

    void ThinkFlow()
    {

    }

    public void GoRandomlyIfShouldAndNotPaused()
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

    public void SetTargetLocationMine()
    {
        targetTransform = locationCollection.GetMine();
    }
    public void SetTargetLocationMainBase()
    {
        targetTransform = locationCollection.GetMainBase();
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

        locationArray = GameObject.FindGameObjectsWithTag(locations);
    }
    public void PickRandomTarget()
    {
        int oldTargetIndex = newTargetIndex;
        CheckAllLocation();
        int i = locationArray.Length;
        newTargetIndex = Random.Range(1, i--);
        //Debug.Log("ArrayLength "+ i);
        i = i++;
        //check if new location is same as old   
        while (newTargetIndex == oldTargetIndex)
        {
            //Debug.Log("Duplicity");
            newTargetIndex = Random.Range(0, i);
        }
        oldTargetIndex = newTargetIndex;
        //Debug.Log("NextTargetIndex: "+ newTargetIndex);
        locationTarget = locationArray[newTargetIndex];
        //Debug.Log("Location Name: " + locationTarget.name);
        targetLocation = locationTarget.name;
        hasArrived = false;
        SetLocationTransform();
        //return target;
    }

    void SetLocationTransform()
    {
        targetTransform = locationTarget.GetComponent<Transform>();
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
}
