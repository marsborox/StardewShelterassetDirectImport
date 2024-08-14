using System.Collections;
using System.Collections.Generic;

using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    
    UnitAi unitAi;
    
    CharacterAnimation characterAnimation;
    CharacterState characterState;
    private void Awake()
    {
        unitAi = GetComponent<UnitAi>();
        characterAnimation=GetComponent<CharacterAnimation>();
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
        characterAnimation.Run();
        //play animation
    }
    public void Stop()
    {
        Vector2 targetposition = transform.position;
    }
}
