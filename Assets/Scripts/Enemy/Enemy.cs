using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    NavMeshAgent enemyAgent;
    EnemyState state = EnemyState.NormalState;
    EnemyState childState = EnemyState.RestState;

    public float restTime;
    public int HP = 100;
    public int exp = 20;

    public GameObject player;        //Ë÷µÐ
    public int attackDistance = 5;

    float restTimer = 0;

    public enum EnemyState
    {
        NormalState,
        MoveState,
        FightState,
        RestState
    }

    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Ãæ³¯Íæ¼ÒË÷µÐ
        if((player.transform.position - this.transform.position).magnitude <= attackDistance)
        {
            this.transform.rotation = Quaternion.LookRotation(player.transform.position - this.transform.position);
        }

        if(state == EnemyState.NormalState)
        {
            if(childState == EnemyState.RestState)
            {
                restTimer += Time.deltaTime;
                if(restTimer > restTime)
                {
                    Vector3 randomPosition = FindRandomPosition();
                    enemyAgent.SetDestination(randomPosition);
                    childState = EnemyState.MoveState;
                }
            }
            else if(childState == EnemyState.MoveState)
            {
                if(enemyAgent.remainingDistance <= 0)
                {
                    restTimer = 0;
                    childState = EnemyState.RestState;
                }
            }
        }
    }

    Vector3 FindRandomPosition()
    {
        Vector3 randomDir = new Vector3(Random.Range(-1, 1f), 0, Random.Range(-1, 1f));
        return transform.position + randomDir.normalized * Random.Range(2, 5);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;

        if(HP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        int count = Random.Range(0, 4);
        for (int i = 0; i < count; i++)
        {
            SpawnPickableItem();
        }
        EventCenter.EnemyDied(this);
        Destroy(this.gameObject);
    }

    void SpawnPickableItem()
    {
        ItemScriptObject item = ItemManager.Instance.GetRandomItem();
        GameObject go = GameObject.Instantiate(item.prefab, transform.position, Quaternion.identity);
        go.tag = Tag.Interactable;
        Animator animator = go.GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
        }

        PickableObject po = go.AddComponent<PickableObject>();
        po.itemScriptObject = item;

        Collider collider = go.GetComponent<Collider>();
        if(collider != null)
        {
            collider.enabled = true;
            collider.isTrigger = false;
        }
        Rigidbody rb = go.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
        go.AddComponent<PickableObject>();
    }
}
