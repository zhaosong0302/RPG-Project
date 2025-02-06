using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : Interactable
{
    bool isPlayerInRange = false;
    public ItemScriptObject itemScriptObject;

    public override void Interact()
    {
        InventoryManager.Instance.AddItem(itemScriptObject);
        Destroy(this.gameObject);
        Debug.Log("F down");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            ShowInteractPromt(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            ShowInteractPromt(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    void ShowInteractPromt(bool show)
    {
        if (show)
        {
            PickUpUI.Instance.Show();
        }
        else
        {
            PickUpUI.Instance.Hide();
        }
    }
}
