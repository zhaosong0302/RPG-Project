using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObject : Interactable
{
    bool isPlayerInRange = false;

    public string npcName;
    public string[] contentList;

    public override void Interact()
    {
        DialogueUI.Instance.Show(npcName, contentList);
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
