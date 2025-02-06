using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PickUpUI : MonoBehaviour
{
    public static PickUpUI Instance { get; private set; }
    GameObject pickupUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Hide();
        }
    }

    private void Start()
    {
        pickupUI = transform.Find("UI").gameObject;
        Hide();
    }

    public void Show()
    {
        pickupUI.SetActive(true);
    }

    public void Hide()
    {
        pickupUI.SetActive(false);
    }
}
