using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField] private EnemyManager enemyManager;
    [SerializeField] private TriggerEnterEvent Event1;


    public void StartEvent1()
    {
        Destroy(Event1.gameObject);
        enemyManager.StartEnemy();
    }
}
