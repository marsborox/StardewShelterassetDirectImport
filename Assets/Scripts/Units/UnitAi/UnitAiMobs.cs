using System.Collections;
using System.Collections.Generic;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;
using UnityEditor.Experimental.GraphView;

using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;
public enum MobsActivity { IDLE, COMBAT,}
public class UnitAiMobs : UnitAiBase
{
    MobsActivity mobsActivity;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unitStatsAndInfo.taskString = mobsActivity.ToString();
        EnemyActivitySwitch();
    }

    public void EnemyActivitySwitch()
    {//only enemy
        switch (mobsActivity)

        {
            case MobsActivity.IDLE:
                {
                    characterAnimation.Idle();
                    break;
                }
            case MobsActivity.COMBAT:
                {
                    unitCombat.Combat();
                    break;
                }
        }//why TF it works without declaration? erhaps it being otuside of class or whatever
        switch (unitCombat.combatActivity) 
        {
            case CombatActivity.IDLE:
                {
                    characterAnimation.Idle();
                    break;
                }
            case CombatActivity.COMBAT:
                {
                    unitCombat.Combat();
                    break;
                }
            case CombatActivity.DEAD:
                {
                    characterAnimation.Die();
                    break;
                }
        }
    }


}
