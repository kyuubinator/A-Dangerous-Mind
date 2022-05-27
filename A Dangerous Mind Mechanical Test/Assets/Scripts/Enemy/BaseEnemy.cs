#region Namespaces/Directives

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

#endregion

public abstract class BaseEnemy : MonoBehaviour
{

    #region Declarations

    [Header("Movement Settings")]
    [SerializeField] private Vector3 _patrolPositionOne;
    [SerializeField] private Vector3 _patrolPositionTwo;
    [SerializeField] protected float _movementSpeed;
    [SerializeField] protected float radius;
    [SerializeField] protected float timeTillResetPath;
    [SerializeField] protected float timerResetPath;

    protected Vector3 _targetPositionWalk;
    protected Vector3 targetPosition;

    #endregion

    #region MonoBehaviour

    protected virtual void Awake()
    {
        targetPosition = _patrolPositionOne;
        _targetPositionWalk = _patrolPositionOne;
    }

    protected virtual void Update()
    {
        Debug.DrawRay(targetPosition, Vector3.up, Color.blue, 6);
        Move();
    }

    #endregion

    public virtual void Attack()
    {

    }

    public virtual void Move()
    {
        
    }
}