using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class AreaUI : MonoBehaviour
{

    [SerializeField] GameObject areas;
    [SerializeField] Button areaButton;
    // Start is called before the first frame update
    void Start()
    {
        DisplayAreaButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        DisplayAreaButtons();
        Debug.Log("Areas Displayed");
    }
    private void OnDisable()
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
    void DisplayAreaButtons()
    {
        int childCount= areas.transform.childCount;
        //foreach Gameobject area in areas create button with method
        for (int i=0; i<childCount; i++) 
        {
            Button spawnedButton = Instantiate(areaButton);
            spawnedButton.transform.parent = this.transform;
            GameObject area = areas.gameObject.transform.GetChild(i).gameObject;
            spawnedButton.gameObject.GetComponentInChildren<TextMeshProUGUI>().text=area.name;
            //spawnedButton.destination=area;
        }
    }
    void AreaButtonMethod()
    { 
        //set target somethign osmething
    }
}
