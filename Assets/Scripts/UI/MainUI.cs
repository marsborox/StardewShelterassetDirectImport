using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI mana;
    public TextMeshProUGUI energy;

    public TextMeshProUGUI name;
    public TextMeshProUGUI xp;
    public TextMeshProUGUI activity;
    public TextMeshProUGUI combatState;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI range;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI attackSpeed;

    public GameObject activeUnit;

    public int temp;

    public void RefreshUI()
    { 
        health.SetText(activeUnit.GetComponent<UnitHealth>().healthCurrent+" / "+ activeUnit.GetComponent<UnitHealth>().healthMax.ToString());


    }
}
