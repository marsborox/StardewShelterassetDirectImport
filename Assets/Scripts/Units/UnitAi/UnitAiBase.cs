using System;
using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;

using UnityEditor.Experimental.GraphView;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;



public class UnitAiBase : MonoBehaviour
{
    //universal class for all units in game
    //public CombatActivity combatActivity;
    

    public UnitMovement unitMovement;
    public UnitCombat unitCombat;
    //UnitStatsAndInfo unitStatsAndInfo;
    //ObjectInfo objectInfo;


    public UnitHealth unitHealth;
    public UnitTargetPicker unitTargetPicker;
    public CharacterAnimation characterAnimation;
    


   
    //bool _targetInRange;
    //[SerializeField] public bool inCombat;


    private void Awake()
    {
        unitMovement = GetComponent<UnitMovement>();
        unitCombat = GetComponent<UnitCombat>();
        //unitStatsAndInfo = GetComponent<UnitStatsAndInfo>();
        unitHealth = GetComponent<UnitHealth>();
        unitTargetPicker = GetComponent<UnitTargetPicker>();
        characterAnimation = GetComponent<CharacterAnimation>();
        //objectInfo = GetComponent<ObjectInfo>();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnEnable()
    {
        unitCombat.underAttack += IfImIdleMakeMeCombat;
    }

    private void OnDisable()
    {
        unitCombat.underAttack -= IfImIdleMakeMeCombat;
    }


    public void IfImIdleMakeMeCombat()
    {//only enemies
        if (unitCombat.combatActivity == CombatActivity.IDLE)
        {
            unitCombat.combatActivity = CombatActivity.COMBAT;
        }
        if (unitCombat.combatActivity == CombatActivity.RESTING)
        {
            unitCombat.combatActivity = CombatActivity.COMBAT;
        }
        unitCombat.inCombat = true;
    }

}
