using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    public ISODatabase itemDB;

    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);   return;
        }
        Instance = this;
    }

    public ItemScriptObject GetRandomItem()
    {
        int randomIndex = Random.Range(0, itemDB.itemList.Count);
        return itemDB.itemList[randomIndex];
    }
}
