using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("BasicInfo")]
    public TextMeshProUGUI health;
    public TextMeshProUGUI mana;
    public TextMeshProUGUI energy;
    [Header("Stats")]
    public TextMeshProUGUI name;
    public TextMeshProUGUI xp;
    public TextMeshProUGUI task;
    public TextMeshProUGUI combatState;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI range;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI attackSpeed;

    [SerializeField]  public  Button equipmentButton;
    bool equipmentOn;
    [SerializeField]public GameObject equipmentUI;


    public GameObject activeUnit;

    public int temp;

    UnitHealth unitHealth;
    UnitStatsAndInfo unitStatsAndInfo;

    private void Awake()
    {
        //gearButton = transform.GetComponent<Button>();
    }
    private void Start()
    {
        equipmentButton.onClick.AddListener(EquipmentButton);
        equipmentOn = false;
        equipmentUI.SetActive(equipmentOn);
    }
    void Update()
    {
        if (activeUnit != null)
        {
            health.SetText(activeUnit.GetComponent<UnitHealth>().healthCurrent + " / " + activeUnit.GetComponent<UnitHealth>().healthMax.ToString());


            //task.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().taskString);

            combatState.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().combatActivityString);

            range.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().range.ToString());
            damage.SetText(activeUnit.GetComponent <UnitStatsAndInfo>().damage.ToString());
            attackSpeed.SetText(activeUnit.GetComponent<UnitStatsAndInfo>().attackSpeed.ToString());
            //attackSpeed.SetText(activeUnit.GetComponent<UnitAiBase>().attackSpeed.ToString());


            task.SetText(activeUnit.GetComponent<UnitAiBase>().GetActivity());// like newest kind of stuff

        }
    
    }
    public void ActiveUnit()
    {
        unitHealth = activeUnit.GetComponent<UnitHealth>();

        unitStatsAndInfo=activeUnit.GetComponent<UnitStatsAndInfo>();
    }
    public void RefreshUI()
    { 
        
        //mana
        //energy
        //name
        //xp
        //activity.SetText(activeUnit.);
    }
    void EquipmentButton()
    {
        Debug.Log("equipmentButton pressed");
        //Color32 color;
        if (!equipmentOn)
        {
            equipmentOn = true;
            //set color pressed
            //set gear window active
            equipmentButton.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            equipmentUI.SetActive(equipmentOn);
        }
        else 
        {
            equipmentOn = false;
            //set color depressed
            //deactivate gear window
            equipmentButton.GetComponent<Image>().color=new Color32(255,255,255,255);
            equipmentUI.SetActive(equipmentOn);
        }
    }
}
