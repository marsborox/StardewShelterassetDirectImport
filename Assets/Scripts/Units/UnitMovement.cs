using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    
    UnitAi unitAi;
    

    private void Awake()
    {
        unitAi = GetComponent<UnitAi>();
    }

    public float GetMoveSpeed()
    { 
        return movementSpeed;
    }

    public void Move(Transform target)
    {

        Vector2 targetPosition = target.position;
        float delta = movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,targetPosition,delta);
        //play animation
    }
    public void Stop()
    {
        Vector2 targetposition = transform.position;
    }
}
