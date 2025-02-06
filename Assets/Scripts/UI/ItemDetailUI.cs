using NUnit.Framework.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    public UnityEngine.UI.Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;
    public TextMeshProUGUI descriptionText;
    public GameObject propertyGrid;
    public GameObject propertyTemplate;

    ItemScriptObject itemScriptObject;
    ItemUI itemUI;

    private void Start()
    {
        propertyTemplate.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void UpdateDetailUI(ItemScriptObject itemScriptObject,ItemUI itemUI)
    {
        this.itemScriptObject = itemScriptObject;
        this.itemUI = itemUI;
        this.gameObject.SetActive(true);

        string type = "";
        switch (itemScriptObject.itemType)
        {
            case ItemType.Weapon:
                type = "Weapon"; break;

            case ItemType.Consumable:
                type = "Consumable"; break;
        }

        iconImage.sprite = itemScriptObject.icon;
        nameText.text = itemScriptObject.name;
        typeText.text = type;
        descriptionText.text = itemScriptObject.description;

        foreach(Transform child in propertyGrid.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Property property in itemScriptObject.propertyList){
            string propertyStr = "";
            string propertyName = "";
            switch (property.propertyType)
            {
                case PropertyType.HPValue:
                    propertyName = "HP:";break;
                case PropertyType.EnergyValue:
                    propertyName = "Energy:"; break;
                case PropertyType.MentalValue:
                    propertyName = "Mental:"; break;
                case PropertyType.SpeedValue:
                    propertyName = "Speed:"; break;
                case PropertyType.AttackValue:
                    propertyName = "Attack:"; break;
                default:
                    break;
            }
            propertyStr += propertyName;
            propertyStr += property.value;
            GameObject go = GameObject.Instantiate(propertyTemplate);
            go.SetActive(true);
            go.transform.SetParent(propertyGrid.transform);
            go.transform.Find("Property").GetComponent<TextMeshProUGUI>().text = propertyStr;
        }
    }

    public void OnUseButtonClick()
    {
        InventoryUI.Instance.OnItemUse(itemScriptObject, itemUI);
        this.gameObject.SetActive(false);
    }
}
