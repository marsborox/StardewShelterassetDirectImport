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

    [Header("HeroButtons")]
    [SerializeField]  public  Button equipmentButton;
    public bool equipmentOn;
    [SerializeField] public GameObject equipmentUI;

    [SerializeField] public Button backpackButton;
    public bool backpackOn;
    [SerializeField] public GameObject backpackUI;

    [SerializeField] public Button testButton;
    public bool testOn;
    [SerializeField] public GameObject testUI;


    [Header("ManagementButtons")]
    [SerializeField] public Button areasButton;
    public bool areasOn;
    [SerializeField] public GameObject areasUI;



    public GameObject activeUnit;

    bool tempVar;

    UnitHealth unitHealth;
    UnitStatsAndInfo unitStatsAndInfo;

    GameObject uiComponent;

    BackPackUI backPackUI;
    private void Awake()
    {
        backPackUI = GetComponent<BackPackUI>();
        //gearButton = transform.GetComponent<Button>();
    }
    private void Start()
    {
        #region old way
        /*
        //eq
        equipmentButton.onClick.AddListener(EquipmentButton);
        equipmentOn = false;
        equipmentUI.SetActive(equipmentOn);
        //backpack
        backpackButton.onClick.AddListener(BackPackButton);
        backpackOn = false;
        backpackUI.SetActive(backpackOn);
        */
        #endregion

        #region HeroButtons
        //eq
        equipmentButton.onClick.AddListener(delegate
        {
            ButtonMethod(equipmentOn, equipmentButton, equipmentUI);
            equipmentOn = tempVar;
            equipmentUI.SetActive(tempVar);
        });
        equipmentOn = false;
        equipmentUI.SetActive(equipmentOn);

        //backpack
        backpackButton.onClick.AddListener(delegate
        {
            ButtonMethod(backpackOn, backpackButton, backpackUI);
            backpackOn = tempVar;
            backpackUI.SetActive(tempVar);
        });
        backpackOn = false;
        backpackUI.SetActive(backpackOn);

        //test
        testButton.onClick.AddListener(delegate
        {
            ButtonMethod(testOn, testButton, testUI);
            testOn = tempVar;
            testUI.SetActive(tempVar);
        });
        testOn = false;
        testUI.SetActive(testOn);
        #endregion

        #region Management Buttons
        //areas
        areasButton.onClick.AddListener(delegate
        {
            ButtonMethod(areasOn, areasButton, areasUI);
            areasOn = tempVar;
            areasUI.SetActive(tempVar);
        });
        areasOn = false;
        areasUI.SetActive(areasOn);
        #endregion
    }
    void Update()
    {
        #region activeUnit set info on screen
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
        #endregion
    }

    public void ActiveUnit()
    {
        unitHealth = activeUnit.GetComponent<UnitHealth>();

        unitStatsAndInfo=activeUnit.GetComponent<UnitStatsAndInfo>();
    }
    public void RefreshUI()
    {
        backPackUI.ReloadSlots();
        //mana
        //energy
        //name
        //xp
        //activity.SetText(activeUnit.);
    }
    void ButtonMethod(bool onOff, Button button, GameObject uiComponent)
    {//could be working but doesn modify global variables will resolve one day
        if (!onOff)
        {
            tempVar = true;
            button.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            uiComponent.SetActive(onOff);
        }
        else
        {
            tempVar = false;
            button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            uiComponent.SetActive(onOff);
        }
    }
    #region Old Technique
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
    void BackPackButton()
    {
        Debug.Log("equipmentButton pressed");
        //Color32 color;
        if (!backpackOn)
        {
            backpackOn = true;
            //set color pressed
            //set gear window active
            backpackButton.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            backpackUI.SetActive(backpackOn);
        }
        else
        {
            backpackOn = false;
            //set color depressed
            //deactivate gear window
            backpackButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            backpackUI.SetActive(backpackOn);
        }
    }
    
    void AreasButton()
    {
        Debug.Log("Areas button pressed");

        if (!areasOn)
        {
            areasOn = true;
            areasButton.GetComponent<Image>().color = new Color32(200, 200, 200, 255);
            areasUI.SetActive(areasOn);
            
        }
        else
        { 
            areasOn = false;
            areasButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            areasUI.SetActive(areasOn);
        }
    }
    #endregion
    
    
}
