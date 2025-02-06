using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class MessageUI : MonoBehaviour
{
    public static MessageUI Instance { get; private set; }

    TextMeshProUGUI messageText;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);    return;
        }
        Instance = this;
    }

    private void Start()
    {
        messageText = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        Hide();
    }

    public void Update()
    {
        if (messageText.enabled)
        {
            Color color = messageText.color;
            float alpha = Mathf.Lerp(color.a, 0, Time.deltaTime);
            messageText.color = new Color(color.r, color.g, color.b, alpha);

            if(alpha == 0)
            {
                messageText.enabled = false;
            }
        }
    }

    public void Show(string message)
    {
        messageText.enabled = true;
        messageText.text = message;
        messageText.color = Color.white;
    }
    public void Hide()
    {
        messageText.enabled = false;
    }
}
