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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }


}
