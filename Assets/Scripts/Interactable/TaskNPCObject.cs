using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class TaskNPCObject : Interactable
{
    bool isPlayerInRange = false;

    public GameTaskSO gameTaskSO;
    public string npcName;
    public string[] contentInTaskExecuting;
    public string[] contentInTaskCompleted;
    public string[] contentInTaskEnd;

    private void Start()
    {
        gameTaskSO.state = GameTaskState.Waiting;
    }

    public override void Interact()
    {
        switch (gameTaskSO.state)
        {
            case GameTaskState.Waiting:
                DialogueUI.Instance.Show(npcName, gameTaskSO.dialogue, OnDialogueEnd);
                MessageUI.Instance.Show("Task get");
                break;
            case GameTaskState.Executing:
                DialogueUI.Instance.Show(npcName, contentInTaskExecuting);
                break;
            case GameTaskState.Completed:
                DialogueUI.Instance.Show(npcName, contentInTaskCompleted, OnDialogueEnd);
                MessageUI.Instance.Show("Task end");

                break;
            case GameTaskState.End:
                DialogueUI.Instance.Show(npcName, contentInTaskEnd);
                break;
            default:
                break;
        }
        DialogueUI.Instance.Show(npcName, gameTaskSO.dialogue, OnDialogueEnd);
    }

    public void OnDialogueEnd()
    {
        switch (gameTaskSO.state)
        {
            case GameTaskState.Waiting:
                gameTaskSO.Start();
                InventoryManager.Instance.AddItem(gameTaskSO.startReward);
                break;
            case GameTaskState.Completed:
                gameTaskSO.End();
                InventoryManager.Instance.AddItem(gameTaskSO.endReward);
                break;
            default:
                break;
        }
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
