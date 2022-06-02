using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Time Out")]
    [SerializeField] private bool timeOut;
    [SerializeField] private bool timeOutAfterRandom;
    [SerializeField] private float timeOutRandom;
    [SerializeField] private float timeOutTimer;
    [Header("Time In")]
    [SerializeField] private bool timeIn;
    [SerializeField] private bool timeInAfterRandom;
    [SerializeField] private float timeInRandom;
    [SerializeField] private float timeInTimer;

    [SerializeField] private GameObject enemy;
    [SerializeField] private WalkingEnemy enemyScript;

    [SerializeField] private int minTimeIn;
    [SerializeField] private int maxTimeIn;
    [SerializeField] private int minTimeOut;
    [SerializeField] private int maxTimeOut;

    public bool TimeOut { get => timeOut; set => timeOut = value; }
    public bool TimeOutAfterRandom { get => timeOutAfterRandom; set => timeOutAfterRandom = value; }

    //private void Start()
    //{
    //    timeOut = true;
    //    timeOutAfterRandom = true;
    //}

    private void Update()
    {
        if (!enemyScript.SawPlayer)
        {
            if (timeOutAfterRandom)
            {
                timeOutRandom = Random.Range(minTimeIn, maxTimeIn);
                timeOutAfterRandom = false;
            }
            if (timeOut)
            {
                timeOutTimer += Time.deltaTime;
                if (timeOutTimer >= timeOutRandom)
                {
                    enemy.SetActive(false);
                    timeOut = false;
                    timeInAfterRandom = true;
                    timeIn = true;
                    timeOutTimer = 0;
                }
            }

            if (timeInAfterRandom)
            {
                timeInRandom = Random.Range(minTimeOut, maxTimeOut);
                timeInAfterRandom = false;
            }
            if (timeIn)
            {
                timeInTimer += Time.deltaTime;
                if (timeInTimer >= timeInRandom)
                {
                    enemy.SetActive(true);
                    timeIn = false;
                    timeOutAfterRandom = true;
                    timeOut = true;
                    timeInTimer = 0;
                }
            }
        }
    }

    public void StartEnemy()
    {
        enemy.SetActive(true);
        timeOut = true;
        timeOutAfterRandom = true;
    }
}
