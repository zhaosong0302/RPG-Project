using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scythe : Weapon
{
    public const string isAttack = "isAttack";
    public int atkValue = 50;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Attack()
    {
        animator.SetTrigger(isAttack);
        Invoke("ResetTrigger", 1);
    }
    private void ResetTrigger()
    {
        animator.ResetTrigger(isAttack);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tag.Enemy)
        {
            other.GetComponent<Enemy>().TakeDamage(atkValue);
        }
    }
}
