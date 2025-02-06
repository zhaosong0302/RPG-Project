using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI typeText;

    ItemScriptObject itemScriptObject;

    public void InitItem(ItemScriptObject itemScriptObject)
    {
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
        this.itemScriptObject = itemScriptObject;
    }

    public void OnClick()
    {
        InventoryUI.Instance.OnItemClick(itemScriptObject, this);
    }
}
