using System.Collections;
using System.Collections.Generic;

using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public class UnitMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;
    
    UnitAi unitAi;
    Rigidbody2D myRigidbody2D;
    CharacterAnimation characterAnimation;
    
    private void Awake()
    {
        unitAi = GetComponent<UnitAi>();
        characterAnimation=GetComponent<CharacterAnimation>();
        //myRigidbody2D=GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        
    }

    public float GetMoveSpeed()
    { 
        return movementSpeed;
    }

    public void Move(GameObject target)
    {
        unitAi.activity = Activity.MOVING;
        Vector2 targetPosition = target.transform.position;

        TurnCorrectDirection(target);

        float delta = movementSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position,targetPosition,delta);
        this.characterAnimation.Run();
    }
    public void Stop()
    {
        Vector2 targetposition = transform.position;
    }
    public void TurnCorrectDirection(GameObject target)
    {
        bool movingLeft = target.transform.position.x < this.transform.position.x;
        if (movingLeft)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }
}
