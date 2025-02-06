using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == Tag.Interactable)
        {
            PickableObject po = collision.gameObject.GetComponent<PickableObject>();
            if(po != null && Input.GetKeyDown(KeyCode.F))
            {
                InventoryManager.Instance.AddItem(po.itemScriptObject);
                Destroy(po.gameObject);
            }
        }
    }
}
