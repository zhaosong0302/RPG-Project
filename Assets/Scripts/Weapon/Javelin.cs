using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Javelin : Weapon
{
    public GameObject javelin;
    public float bulletSpeed;

    GameObject bullet;

    private void Start()
    {
        SpawnBullet();
    }

    public override void Attack()
    {
        if (bullet != null)
        {
            bullet.transform.parent = null;
            bullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
            bullet.GetComponent<Collider>().enabled = true;
            Destroy(bullet, 10);
            bullet = null;
            Invoke("SpawnBullet", 0.5f);
        }
        else
        {
            return;
        }
    }

    void SpawnBullet()
    {
        bullet = GameObject.Instantiate(javelin, transform.position, transform.rotation);
        bullet.transform.parent = transform;
        bullet.GetComponent<Collider>().enabled = false;
        if(tag == Tag.Interactable)
        {
            Destroy(bullet.GetComponent<JavelinBullet>());

            bullet.tag = Tag.Interactable;
            PickableObject po = bullet.AddComponent<PickableObject>();
            po.itemScriptObject = GetComponent<PickableObject>().itemScriptObject;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            rb.constraints = ~RigidbodyConstraints.FreezeAll;
            bullet.GetComponent<Collider>().enabled = true;
            bullet.transform.parent = null;
            Destroy(this.gameObject);
        }
    }
}
