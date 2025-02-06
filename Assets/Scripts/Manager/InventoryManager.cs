using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);    return;
        }
        Instance = this;
    }

    public List<ItemScriptObject> itemList;
    public ItemScriptObject defaultWeapon;

    public void AddItem(ItemScriptObject itemScriptObject)
    {
        itemList.Add(itemScriptObject);
        InventoryUI.Instance.AddItem(itemScriptObject);

        MessageUI.Instance.Show("You get a " + itemScriptObject.name);
    }
    public void RemoveItem(ItemScriptObject itemScriptObject)
    {
        itemList.Remove(itemScriptObject);
    }
}
