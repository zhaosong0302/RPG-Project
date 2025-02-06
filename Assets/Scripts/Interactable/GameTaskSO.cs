using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameTaskState
{
    Waiting,
    Executing,
    Completed,
    End
}

[CreateAssetMenu()]
public class GameTaskSO : ScriptableObject
{
    public GameTaskState state;
    public string[] dialogue;
    public ItemScriptObject startReward;
    public ItemScriptObject endReward;
    public int enemyCountNeed = 5;
    public int currentEnemyCount = 0;

    public void Start()
    {
        currentEnemyCount = 0;
        state = GameTaskState.Executing;
        EventCenter.OnEnemyDied += OnEnemyDied;
    }

    void OnEnemyDied(Enemy enemy)
    {
        if (state == GameTaskState.Completed) return;
        currentEnemyCount++;
        if(currentEnemyCount >= enemyCountNeed)
        {
            state = GameTaskState.Completed;
            MessageUI.Instance.Show("Task completed");
        }
    }
    public void End()
    {
        state = GameTaskState.End;
        EventCenter.OnEnemyDied -= OnEnemyDied;
    }
}
