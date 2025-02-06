using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavelinBullet : MonoBehaviour
{
    public int atkValue = 30;

    Rigidbody rb;
    new Collider collider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == Tag.Player) { return; }

        rb.isKinematic = true;
        collider.enabled = false;

        transform.parent = collision.gameObject.transform;

        Destroy(this.gameObject, 3f);

        if(collision.gameObject.tag == Tag.Enemy)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(atkValue);
        }
    }
}