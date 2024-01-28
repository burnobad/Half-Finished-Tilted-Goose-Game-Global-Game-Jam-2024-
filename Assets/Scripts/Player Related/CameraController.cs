using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    
    [SerializeField]
    private Transform player;

    [SerializeField, Range(100, 800)]
    private float mouseSensY = 400;

    private Vector3 startPos;
    private Quaternion startRot;

    private float circleRadius;
    private float xRotation;


    private void Awake()
    {
        startPos = transform.position - player.position;
        startRot = transform.localRotation;

        float zSquare = Mathf.Pow(startPos.z, 2);
        float ySquare = Mathf.Pow(startPos.y, 2);
        circleRadius = Mathf.Sqrt(zSquare + ySquare);
    }
    private void LateUpdate()
    { 
        Vector3 cameraStartPos =
            player.forward * startPos.z + player.right * startPos.x + player.up * startPos.y;


        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensY;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f - startRot.eulerAngles.x, 90f - startRot.eulerAngles.x);

        transform.localRotation = Quaternion.Euler(0, player.localEulerAngles.y, 0f) * startRot;

        transform.position = player.position + cameraStartPos;
    }
}
