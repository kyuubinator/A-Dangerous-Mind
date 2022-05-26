using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : BaseEnemy
{
    #region Declarations

    [SerializeField] private Transform player;
    [SerializeField] private float detectRange;
    [SerializeField] private float timeTillReset;
    [SerializeField] private float timer;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private bool sawPlayer;
    [Header("Timers")]

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


    #endregion

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        timeOutAfterRandom = true;
        timeOut = true;
    }

    protected override void Update()
    {
        base.Update();
        if (timeOutAfterRandom)
        {
            timeOutRandom = Random.Range(0, 240);
            timeOutAfterRandom = false;
        }
        if (timeOut)
        {
            timeOutTimer += Time.deltaTime;
            if (timeOutTimer >= timeOutRandom)
            {
                this.gameObject.SetActive(false);
                timeOut = false;
                timeInAfterRandom = true;
                timeIn = true;
            }
        }

        if (timeInAfterRandom)
        {
            timeInRandom = Random.Range(0, 240);
            timeInAfterRandom = false;
        }
        if (timeIn)
        {
            timeInTimer += Time.deltaTime;
            if (timeInTimer >= timeInRandom)
            {
                this.gameObject.SetActive(true);
                timeIn = false;
                timeOutAfterRandom = true;
                timeOut = true;
            }
        }
    }

    public override void Attack()
    {

    }

    public override void Move()
    {
        Vector3 _direction = player.position - transform.position;
        transform.LookAt(_targetPosition);

        if (Vector3.Distance(transform.position, player.transform.position) < detectRange)
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position, _direction, Color.red, detectRange);
            Physics.Raycast(transform.position, _direction, out hit, detectRange);
            if(hit.transform == null) { return; }
            PlayerCharacter playerScript = hit.transform.GetComponent<PlayerCharacter>();
            if (playerScript != null)
            {
                if (!sawPlayer)
                {
                    sawPlayer = true;
                }
                _targetPosition = playerScript.transform.position;
                agent.SetDestination(_targetPosition);
                if (Vector3.Distance(transform.position, player.transform.position) < detectRange / 4)
                {
                    agent.ResetPath();
                }
                timer = 0;
            }
            else
            {
                NotSeeingPlayer();
                return;
            }
        }
        else
        {
            if (sawPlayer)
            {
                NotSeeingPlayer();
            }
            agent.SetDestination(_targetPosition);
            base.Move();
        }
    }

    private void NotSeeingPlayer()
    {
        if (Vector3.Distance(_targetPosition, agent.transform.position) <= 0.2f)
        {
            agent.ResetPath();
            timer += Time.deltaTime;
            if (timer > timeTillReset)
            {
                agent.ResetPath();
                timer = 0;
                sawPlayer = false;
                _targetPosition = _targetPositionWalk;
                agent.SetDestination(_targetPosition);
                base.Move();
            }
        }
        else
        {
            if (sawPlayer)
            {
                timer += Time.deltaTime;
                if (timer > timeTillReset)
                {
                    agent.ResetPath();
                    timer = 0;
                    sawPlayer = false;
                    _targetPosition = _targetPositionWalk;
                    agent.SetDestination(_targetPosition);
                    base.Move();
                }
            }
        }
    }
}