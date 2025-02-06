using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyDied;

    public static void EnemyDied(Enemy enemy)
    {
        if(OnEnemyDied != null)
        {
            OnEnemyDied.Invoke(enemy);
        }
    }
}
