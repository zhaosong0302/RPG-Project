using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPropertyUI : MonoBehaviour
{
    public static PlayerPropertyUI Instance { get; private set; }

    GameObject uiGameObject;
    UnityEngine.UI.Image hpProgressBar;
    TextMeshProUGUI hpText;
    UnityEngine.UI.Image levelProgressBar;
    TextMeshProUGUI levelText;
    GameObject propertyGrid;
    GameObject propertyTemplate;
    UnityEngine.UI.Image weaponIcon;
    PlayerProperty pp;
    PlayerAttack pa;

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
        uiGameObject = transform.Find("UI").gameObject;
        hpProgressBar = transform.Find("UI/HPProgressBar/ProgressBar").GetComponent<Image>();
        hpText = transform.Find("UI/HPProgressBar/HPText").GetComponent<TextMeshProUGUI>();
        levelProgressBar = transform.Find("UI/LevelProgressBar/ProgressBar").GetComponent<Image>();
        levelText = transform.Find("UI/LevelProgressBar/LevelText").GetComponent<TextMeshProUGUI>();
        propertyGrid = transform.Find("UI/PropertyGrid").gameObject;
        propertyTemplate = transform.Find("UI/PropertyGrid/PropertyTemplate").gameObject;
        weaponIcon = transform.Find("UI/WeaponIcon").GetComponent<Image>();

        propertyTemplate.SetActive(false);

        GameObject player = GameObject.FindGameObjectWithTag(Tag.Player);
        pp = player.GetComponent<PlayerProperty>();
        pa = player.GetComponent<PlayerAttack>();
        UpdatePlayerPropertyUI();
        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (uiGameObject.activeSelf)
            {
                Hide();
            }
            else { Show(); }
        }
    }

    public void UpdatePlayerPropertyUI()
    {
        hpProgressBar.fillAmount = pp.hpValue / 100.0f;
        hpText.text = pp.hpValue + "/100";
        levelProgressBar.fillAmount = pp.currentExp * 1.0f / (pp.level * 30);
        levelText.text = pp.level.ToString();

        ClearGrid();

        AddProperty("Energy:" + pp.energyValue);
        AddProperty("Mental:" + pp.mentalValue);

        foreach(var item in pp.propertyDict)
        {
            string propertyName = "";
            switch (item.Key)
            {
                case PropertyType.HPValue:
                    propertyName = "HP:"; break;
                case PropertyType.EnergyValue:
                    propertyName = "Energy:"; break;
                case PropertyType.MentalValue:
                    propertyName = "Mental:"; break;
                case PropertyType.SpeedValue:
                    propertyName = "Speed:"; break;
                case PropertyType.AttackValue:
                    propertyName = "Attack:"; break;
                default:
                    break;
            }

            int sum = 0;
            foreach (var item1 in item.Value)
            {
                sum += item1.value;
            }
            AddProperty(propertyName + sum);
        }

        if(pa.weaponIcon != null)
        {
            weaponIcon.sprite = pa.weaponIcon;
        }
    }

    void ClearGrid()
    {
        foreach (Transform child in propertyGrid.transform)
        {
            if (child.gameObject.activeSelf)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void AddProperty(string propertyStr)
    {
        GameObject go = GameObject.Instantiate(propertyTemplate);
        go.SetActive(true);
        go.transform.SetParent(propertyGrid.transform);
        go.transform.Find("Property").GetComponent<TextMeshProUGUI>().text = propertyStr;
    }

    void Show()
    {
        uiGameObject.SetActive(true);
    }
    void Hide()
    {
        uiGameObject.SetActive(false);
    }
}
