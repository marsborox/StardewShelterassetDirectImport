using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
//fuckallhasbeendoneonthisday
    [SerializeField] float healthMax;
    [SerializeField] float healthCurrent;

    float testHealth;
    [SerializeField] float healthFraction;

    [SerializeField] Image healthBarSprite;

    UnitStatsAndInfo unitStatsAndInfo;
    // Start is called before the first frame update
    private void Awake()
    {
        unitStatsAndInfo=GetComponent<UnitStatsAndInfo>();

    }
    void Start()
    {
        healthMax = unitStatsAndInfo.health;
        healthCurrent = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
        
        ControlHealthBarSize();
    }

    public void Die()
    {
        gameObject.transform.parent.transform.parent.GetComponent<ObjectSpawner>().UnitDied();
        

        Debug.Log("I am dying");
        Destroy(this.gameObject);
    }
    public void TakeDamage()
    {
        

        //we will pass value into this method
        //some health = health - received dmg
        //if hp =<1 die
        Die();
        
        
    }
    void ControlHealthBarSize()
    {
        healthFraction = healthCurrent / healthMax;
        healthBarSprite.fillAmount = healthFraction;
    }
}
