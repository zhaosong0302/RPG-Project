using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;
    public Button continueBotton;

    List<string> contentList;
    int contentIndex = 0;
    Action OnDialogueEnd;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);return;
        }
        Instance = this;
    }

    private void Start()
    {
        Hide();
        continueBotton.onClick.AddListener(this.OnContinueButtonClick);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Show(string name, string[] content, Action OnDialogueEnd = null)
    {
        nameText.text = name;
        contentList = new List<string>();
        contentList.AddRange(content);
        contentText.text = contentList[0];
        gameObject.SetActive(true);
        this.OnDialogueEnd = OnDialogueEnd;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void OnContinueButtonClick()
    {
        contentIndex++;
        if (contentIndex >= contentList.Count)
        {
            OnDialogueEnd?.Invoke();
            contentIndex = 0;
            Hide();
            return;
        }
        contentText.text = contentList[contentIndex];
    }
}
