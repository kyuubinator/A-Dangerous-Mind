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
    [Header("Hiding")]
    [SerializeField] private Hide hide;
    [SerializeField] private LayerMask hideLayer;
    private bool hidden;

    [SerializeField] private UIManager ui;

    public bool SawPlayer { get => sawPlayer; }


    #endregion


    #region Mono Behaviour
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    protected override void Update()
    {
        base.Update();
        if (!sawPlayer)
        {
            timerResetPath += Time.deltaTime;
        }
        if (timerResetPath >= timeTillResetPath)
        {
            RandomLocation();
            timerResetPath = 0;
        }
        if (sawPlayer)
        {
            agent.speed = 5f;
        }
        else
        {
            agent.speed = 1f;
        }
    }

    #endregion

    #region Movement/followplayer
    public override void Move()
    {
        Vector3 _direction = player.position - transform.position;
        transform.LookAt(targetPosition);

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
                    hidden = false;
                    sawPlayer = true;
                }
                targetPosition = playerScript.transform.position;
                agent.SetDestination(targetPosition);
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
            agent.SetDestination(targetPosition);
            NormalMovement();
        }
    }

    public void NormalMovement()
    {
        if (targetPosition != null)
        {
            transform.LookAt(targetPosition);
        }
        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            RandomLocation();
        }
    }
    #endregion

    #region Not Seeing player
    private void NotSeeingPlayer()
    {
        if (Vector3.Distance(targetPosition, agent.transform.position) <= 0.05f)
        {
            agent.ResetPath();
            timer += Time.deltaTime;

            #region CheckHiding

            if (timer >= timeTillReset){
                if (!hidden)
                {
                    Collider[] colider = Physics.OverlapSphere(transform.position, 10, hideLayer);
                    foreach (Collider col in colider)
                    {
                        Hide hideScript = col.GetComponent<Hide>();
                        if (hideScript != null)
                        {
                            hide = hideScript;
                        }
                    }
                    float random = Random.Range(1, 10);
                    if (random <= 7)
                    {
                        hide.Unhide();
                        hide = null;
                    }
                    hidden = true;
                }
            }

            #endregion

            if (timer > timeTillReset)
            {
                agent.ResetPath();
                timer = 0;
                sawPlayer = false;
                RandomLocation();
                base.Move();
                hidden = false;
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
                    RandomLocation();
                    base.Move();
                }
            }
        }
    }

    #endregion

    public void RandomLocation()
    {
        Vector3 randomDirection = (Random.insideUnitSphere * radius);
        randomDirection = new Vector3(randomDirection.x, transform.position.y, randomDirection.z);
        NavMeshPath path = new NavMeshPath();
        agent.CalculatePath(randomDirection, path);
        if (path.status == NavMeshPathStatus.PathInvalid)
        {
            RandomLocation();
            return;
        }
        else
            targetPosition = randomDirection;
        agent.SetDestination(targetPosition);
        timerResetPath = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerCharacter player = collision.gameObject.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            ui.ShowLose();
        }
    }
}