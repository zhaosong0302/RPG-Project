using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController player;
    PlayerAttack playerAttack;
    PlayerProperty playerProperty;


    public float speed = 3f;
    public float rotationSpeed = 720f;

    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAttack = GetComponent<PlayerAttack>();
        playerProperty = GetComponent<PlayerProperty>();
    }

    // Update is called once per frame
    void Update()
    {
        //ˮƽ��
        float horizontal = Input.GetAxis("Horizontal");
        //��ֱ��
        float vertical = Input.GetAxis("Vertical");
        //����һ����������
        Vector3 dir = new Vector3(horizontal, 0, vertical);
        //���ƶ����³���
        if(dir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        //�ƶ�
        player.SimpleMove(dir * speed);
    }

    public void UseItem(ItemScriptObject itemScriptObject)
    {
        switch (itemScriptObject.itemType)
        {
            case ItemType.Weapon:
                playerAttack.LoadWeapon(itemScriptObject);
                break;
            case ItemType.Consumable:
                playerProperty.UseDrag(itemScriptObject);
                break;
            default:
                break;
        }
    }
}
