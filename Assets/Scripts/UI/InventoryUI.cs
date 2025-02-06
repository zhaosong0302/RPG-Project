using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    public GameObject itemPrefab;
    public bool isShow = false;
    public ItemDetailUI itemDetailUI;
    
    GameObject uiGamObject;
    GameObject content;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        uiGamObject = transform.Find("UI").gameObject;
        content = transform.Find("UI/ListBackground/Scroll View/Viewport/Content").gameObject;
        Hide();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isShow)
            {
                Hide();
                isShow = false;
            }
            else
            {
                Show();
                isShow = true;
            }
        }
    }

    public void Show()
    {
        uiGamObject.SetActive(true);
    }
    public void Hide()
    {
        uiGamObject.SetActive(false);
    }

    public void AddItem(ItemScriptObject itemScriptObject)
    {
        GameObject itemGO = GameObject.Instantiate(itemPrefab);
        itemGO.transform.parent = content.transform;
        ItemUI itemUI = itemGO.GetComponent<ItemUI>();

        itemUI.InitItem(itemScriptObject);
    }

    public void OnItemClick(ItemScriptObject itemScriptObject, ItemUI itemUI)
    {
        itemDetailUI.UpdateDetailUI(itemScriptObject, itemUI);
    }

    public void OnItemUse(ItemScriptObject itemScriptObject, ItemUI itemUI)
    {
        Destroy(itemUI.gameObject);
        InventoryManager.Instance.RemoveItem(itemScriptObject);

        GameObject.FindGameObjectWithTag(Tag.Player).GetComponent<Player>().UseItem(itemScriptObject);
    }
}
