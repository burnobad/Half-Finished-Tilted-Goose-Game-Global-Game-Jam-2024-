using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    

    [SerializeField]
    private Transform cameraTarget;
    [SerializeField]
    private Transform player;

    private Vector3 startPos;

    private void Awake()
    {
        startPos = transform.position;

   
    }
    private void LateUpdate()
    { 

        Vector3 cameraPos = player.forward * startPos.z + player.right * startPos.x + player.up * startPos.y;



        transform.position = player.position + cameraPos;

        transform.LookAt(cameraTarget);
    }
}
