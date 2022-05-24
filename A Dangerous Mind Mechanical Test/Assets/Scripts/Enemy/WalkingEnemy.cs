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


    #endregion

    private void Start()
    {
        player = GameObject.Find("Player").transform;
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
            PlayerCharacter playerScript = hit.transform.GetComponent<PlayerCharacter>();
            if (playerScript != null)
            {
                if (!sawPlayer)
                    sawPlayer = true;

                _targetPosition = playerScript.transform.position;
                agent.SetDestination(_targetPosition);
                if (Vector3.Distance(transform.position, player.transform.position) < detectRange / 4)
                {
                    agent.ResetPath();
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
        else
        {
            agent.SetDestination(_targetPosition);
            base.Move();
        }
    }
}