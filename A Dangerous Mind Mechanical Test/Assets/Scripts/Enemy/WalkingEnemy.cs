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
    [SerializeField] private bool hidden;

    [SerializeField] private UIManager ui;
    [SerializeField] private float distance;
    public bool SawPlayer { get => sawPlayer; }


    #endregion


    #region Mono Behaviour
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    protected override void Update()
    {
        distance = Vector3.Distance(player.transform.position, agent.transform.position);
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
            if(hit.transform == null) 
            {
                if (sawPlayer)
                {
                    NotSeeingPlayer();
                }
                return;
            }
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
                agent.speed = 5f;
            }
            else
            {
                if (sawPlayer)
                {
                    NotSeeingPlayer();
                }
                return;
            }
        }
        else
        {
            if (sawPlayer)
            {
                NotSeeingPlayer();
            }
            else
            {
                agent.SetDestination(targetPosition);
                NormalMovement();
            }
        }
    }

    public void NormalMovement()
    {
        if (targetPosition != null)
        {
            transform.LookAt(targetPosition);
        }
        if (Vector3.Distance(transform.position, targetPosition) < 0.6f)
        {
            RandomLocation();
        }
    }
    #endregion

    #region Not Seeing player
    private void NotSeeingPlayer()
    {
        timer += Time.deltaTime;
        agent.speed = 3f;
        if (distance <= 1.5f)
        {
            //agent.angularSpeed = 0;
            //agent.speed = 0;
            agent.ResetPath();
            //timer += Time.deltaTime;
            Debug.Log("Before Hide");
            #region CheckHiding

            if (timer >= timeTillReset){
                Debug.Log("trying to unhide");
                if (!hidden)
                {
                    Debug.Log("hidden = false");
                    Collider[] colider = Physics.OverlapSphere(transform.position, 7, hideLayer);
                    foreach (Collider col in colider)
                    {
                        Hide hideScript = col.GetComponent<Hide>();
                        if (hideScript != null)
                        {
                            hide = hideScript;
                        }
                    }
                    float random = Random.Range(1, 10);
                    Debug.Log("after random");
                    if (random <= 7)
                    {
                        Debug.Log(" 70%");
                        if (hide != null)
                            hide.Unhide();
                            Debug.Log(" Not null + Correu Unhide");
                        Debug.Log(" hide = null");
                    }
                    else
                    {
                        RandomLocation();
                    }
                    hide = null;
                    hidden = true;
                    Debug.Log("Hidden true");
                }
            }
            if (Vector3.Distance(transform.position, targetPosition) < 1f)
            {
                agent.ResetPath();
            }
                #endregion
                Debug.Log("after Hide");
            if (timer >= timeTillReset)
            {
                //agent.ResetPath();
                timer = 0;
                sawPlayer = false;
                RandomLocation();
                base.Move();
                hidden = false;
                Debug.Log("Reset Path");
            }
        }
        else
        {
            if (sawPlayer)
            {
                //timer += Time.deltaTime;
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
        Debug.Log("RandomLocation");
        agent.speed = 1f;
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