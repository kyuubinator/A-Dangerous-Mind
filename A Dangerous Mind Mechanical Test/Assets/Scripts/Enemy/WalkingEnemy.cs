using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkingEnemy : BaseEnemy
{
    #region Declarations

    [SerializeField] private Transform player;
    [SerializeField] private float detectRange;
    [SerializeField] public float knockBack;
    [SerializeField] private float cooldownTimer;
    [SerializeField] private float timer;
    [SerializeField] public NavMeshAgent agent;


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
                _targetPosition = playerScript.transform.position;
                timer += Time.deltaTime;
                agent.SetDestination(_targetPosition);
                if (timer > cooldownTimer)
                {
                    Attack();
                    timer = 0;
                }
                if (Vector3.Distance(transform.position, player.transform.position) < detectRange / 4)
                {
                    agent.ResetPath();
                }

            }
            else
            {
                agent.SetDestination(_targetPosition);
                base.Move();
            }
        }
        else
        {
            agent.SetDestination(_targetPosition);
            base.Move();
        }
    }
}