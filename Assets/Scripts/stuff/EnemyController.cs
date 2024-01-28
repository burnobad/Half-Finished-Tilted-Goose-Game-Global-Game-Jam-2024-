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
    private LayerMask playerLayer;

    #endregion

    #region Serializeble Variables
    [Header("Serializeble Variables")]

    [SerializeField, Range(1, 8)]
    private float moveSpeed;

    [SerializeField, Range(1, 8)]
    private float detectionRadius;

    #endregion

    #region Private Variables

    private Transform target;

    #endregion

    void Awake()
    {
        target = null;
        agent.speed = moveSpeed;
    }

    void FixedUpdate()
    {
        if(target != null)
        {
            agent.destination = target.position;
        }
        else 
        {
            Collider[] playerDetection = 
                Physics.OverlapSphere(transform.position + transform.forward * detectionRadius / 2, detectionRadius, playerLayer);

            if(playerDetection.Length > 0 ) 
            {
                target = playerDetection[0].transform;
            }

        }
        

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position + transform.forward * detectionRadius / 2, detectionRadius);

    }
}
