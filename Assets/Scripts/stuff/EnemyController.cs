using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    #region Components
    [Header("Components")]

    [SerializeField]
    private NavMeshAgent agent;

    [SerializeField]
    private Collider coll;

    [SerializeField]
    private LayerMask playerLayer;

    #endregion

    #region Serializeble Variables
    [Header("Serializeble Variables")]

    [SerializeField, Range(1, 8)]
    private float moveSpeed;

    [SerializeField, Range(1, 8)]
    private float detectionRadius;

    [SerializeField, Range(1, 3)]
    private int maxHealth;

    #endregion

    #region Private Variables

    private float currentHP;
    private Transform target;
    
    private enum State { Move, Damaged, Dead}
    private State state;

    #endregion

    void Awake()
    {
        target = null;
        agent.speed = moveSpeed;
        state = State.Move;
        currentHP = maxHealth;
    }

    void FixedUpdate()
    {
        switch (state)
        {
            case State.Move:
                MoveState();
                break;
            case State.Damaged:
                DamagedState();
                break;
            case State.Dead:
                DeadState();
                break;
        }    

    }

    public void GetDamaged()
    {
        currentHP--;

        if(currentHP <= 0)
        {
            state = State.Dead;
            Debug.Log(state);
        }

        target = PlayerController.Instance.transform;
    }

    #region State Behavioue
    void MoveState()
    {
        if (target != null)
        {
            agent.destination = target.position;
        }
        else
        {
            Collider[] playerDetection =
                Physics.OverlapSphere(transform.position + transform.forward * detectionRadius / 2, detectionRadius, playerLayer);

            if (playerDetection.Length > 0)
            {
                target = playerDetection[0].transform;
            }

        }
    }

    void DamagedState()
    {

    }

    void DeadState()
    {
        coll.enabled = false;
    }

    #endregion
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + transform.forward * detectionRadius / 2, detectionRadius);

    }
}
