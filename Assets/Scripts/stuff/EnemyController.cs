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
    private Rigidbody rb;

    [SerializeField]
    private NavMeshAgent agent;

    #endregion

    #region Serializeble Variables
    [Header("Serializeble Variables")]

    [SerializeField, Range(1, 8)]
    private float moveSpeed;

    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
