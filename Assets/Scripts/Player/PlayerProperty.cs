using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class PlayerProperty : MonoBehaviour
{
    public Dictionary<PropertyType, List<Property>> propertyDict;
    public int hpValue = 100, energyValue = 100, mentalValue = 100;
    public int level = 1, currentExp = 0;

    void Awake()
    {
        propertyDict = new Dictionary<PropertyType, List<Property>>();
        propertyDict.Add(PropertyType.SpeedValue, new List<Property>());
        propertyDict.Add(PropertyType.AttackValue, new List<Property>());

        AddProperty(PropertyType.SpeedValue, 5);
        AddProperty(PropertyType.AttackValue, 5);

        EventCenter.OnEnemyDied += OnEnemyDied;
    }

    public void UseDrag(ItemScriptObject itemScriptObject)
    {
        foreach(Property p in itemScriptObject.propertyList)
        {
            AddProperty(p.propertyType, p.value);
        }
    }

    public void AddProperty(PropertyType pt, int value)
    {
        switch (pt)
        {
            case PropertyType.HPValue :
                hpValue += value;   return;
            case PropertyType.EnergyValue:
                energyValue += value;   return;
            case PropertyType.MentalValue:
                mentalValue += value;   return;
        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);
        list.Add(new Property(pt, value));
    }
    public void RemoveProperty(PropertyType pt, int value)
    {
        switch (pt)
        {
            case PropertyType.HPValue:
                hpValue -= value; return;
            case PropertyType.EnergyValue:
                energyValue -= value; return;
            case PropertyType.MentalValue:
                mentalValue -= value; return;
        }

        List<Property> list;
        propertyDict.TryGetValue(pt, out list);

        list.Remove(list.Find(x => x.value == value));
    }

    private void OnDestroy()
    {
        EventCenter.OnEnemyDied -= OnEnemyDied;
    }

    void OnEnemyDied(Enemy enemy)
    {
        this.currentExp += enemy.exp;

        if(currentExp >= level * 30)
        {
            currentExp -= level * 30;
            level++;
        }
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }
}
