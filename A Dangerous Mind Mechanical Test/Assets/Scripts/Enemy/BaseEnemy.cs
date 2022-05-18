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

    protected Vector3 _targetPositionWalk;
    protected Vector3 _targetPosition;

    #endregion

    #region MonoBehaviour

    protected virtual void Awake()
    {
        _targetPosition = _patrolPositionOne;
        _targetPositionWalk = _patrolPositionOne;
    }

    private void Update()
    {
        Move();
    }

    #endregion

    public abstract void Attack();

    public virtual void Move()
    {
        transform.LookAt(_targetPosition);
        if (Vector3.Distance(transform.position, _targetPosition) < 0.6f)
        {
            if (_targetPositionWalk == _patrolPositionOne)
            {
                _targetPosition = _patrolPositionTwo;
                _targetPositionWalk = _patrolPositionTwo;
            }
            else
            {
                _targetPosition = _patrolPositionOne;
                _targetPositionWalk = _patrolPositionOne;
            }
        }
    }
}