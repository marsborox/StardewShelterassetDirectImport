using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTargetPicker : MonoBehaviour
{
    List<GameObject> objectList = new List<GameObject>();
    List<GameObject> enemyList = new List<GameObject>();
    [SerializeField] public GameObject target;
    [SerializeField] string targetName;

    //redo stuff 
    //public string enemyTag = "EnemyUnit";
    public string enemyTag = "EnemyUnit";

    public void FindClosestEnemy()
    {
        //Debug.Log("FINDING ENEMIES");
        ListObjectsInMap();
        ListEnemies();
        GetClosestObject(enemyList);
    }


    void ListObjectsInMap()
    {
        //Debug.Log("Listing objects");
        objectList.Clear();
        //Debug.Log("ObjectList cleared");

        Transform parentTransform = this.transform.parent;
        if (parentTransform != null)//this is just to hceck if its null to avoid crash
        {
            foreach (Transform transformObject in parentTransform)
            {
                objectList.Add(transformObject.gameObject);
            }
            //Debug.Log($"Listing objects OK, listCount: "+objectList.Count);
        }
        else
        {
            Debug.LogWarning("Parent transform is null. No objects to list.");
        }
    }
    void ListEnemies()
    {
        //Debug.Log("Listing enemies");
        enemyList.Clear();
        foreach (GameObject gameObject in objectList)
        {
            //Debug.Log($"Object: {gameObject.name}, Tag: {gameObject.tag}");
            if (gameObject.tag == enemyTag)
            {
                enemyList.Add(gameObject);
                //Debug.Log("logged enemy into enemyList");
            }
        }
        //Debug.Log("Listing enemies OK");
    }
    /*
    void ListEnemies()
    {
        //Debug.Log("Listing enemies");
        enemyList.Clear();
        foreach (GameObject gameObject in objectList)
        {
            Debug.Log("noticed object in objectList");
            if (gameObject.tag == enemyTag)
            { 
                enemyList.Add(gameObject);
                Debug.Log("logged enemy into enemyList");
            }
        }
        Debug.Log($"Listing enemies OK, listCount is: "+enemyList.Count);
    }
    */
    GameObject GetClosestObject(List<GameObject> providedList)
    {//this may be broken
        //Debug.Log("Finding target");
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        //Debug.Log($"log entry prior to search in list+ listcount: "+providedList.Count);
        
        foreach (GameObject potentialTarget in providedList)
        {
            //Debug.Log("finding target in provided list");
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

       // Debug.Log("Finding target OK");
        target = bestTarget;
        if (target == null)
        { 
            Debug.Log("target NULL in unit target picker");
            //FindClosestEnemy();
        }
        //Debug.Log(target.name);
        //returning closest gameObject
        targetName = target.name;
        //Debug.Log($"target returned:" +targetName);
        return target;
        
    }
    void TargetDistance()
    { 
        
    }
}
