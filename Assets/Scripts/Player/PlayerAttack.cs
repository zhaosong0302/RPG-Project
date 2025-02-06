using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Weapon weapon;
    public InventoryUI inventoryUI;
    public Sprite weaponIcon;

    private void Update()
    {
        if (weapon != null && Input.GetMouseButtonDown(0) && !inventoryUI.isShow)
        {
            weapon.Attack();
        }
    }

    public void LoadWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
    public void LoadWeapon(ItemScriptObject itemScriptObject)
    {
        if(weapon != null)
        {
            Destroy(weapon.gameObject);
            weapon = null;
        }

        string prefabName = itemScriptObject.prefab.name;
        Transform weaponParent = transform.Find(prefabName + "Position");
        GameObject weaponGO = GameObject.Instantiate(itemScriptObject.prefab);
        weaponGO.transform.parent = weaponParent;
        weaponGO.transform.localPosition = Vector3.zero;
        weaponGO.transform.localRotation = Quaternion.identity;

        this.weapon = weaponGO.GetComponent<Weapon>();
        this.weaponIcon = itemScriptObject.icon;
        PlayerPropertyUI.Instance.UpdatePlayerPropertyUI();
    }
    public void UnLoadWeapon(Weapon weapon)
    {
        weapon = null;
    }
}
