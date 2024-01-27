using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    [Header("Components")]

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private Transform cameraTarget;

    #endregion

    #region Serializeble Variables
    [Header("Serializeble Variables")]

    [SerializeField, Range(1, 8)]
    private float moveSpeed;

    [SerializeField, Range(1, 6)]
    private float jumpForce;

    [SerializeField, Range(0.01f, 1)]
    private float isGroundedLenght;

    #endregion

    #region Private Variables

    private const float BUTTON_PRESS_TIME = 0.23f;

    private Vector3 inputDir;
    private Vector3 moveDir;

    private float jumpElapsedTime;

    #endregion

    private void Awake()
    {
        inputDir = Vector3.zero;
        moveDir = Vector3.zero;

        jumpElapsedTime = 0;   
    }

    void Update()
    {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            jumpElapsedTime = BUTTON_PRESS_TIME;
        }

        moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        moveDir.Normalize();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);

        if (CanJump())
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); 
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        ManageElapsedTime(ref jumpElapsedTime);

    }

    #region Calculated Variable and Checks

    bool CanJump()
    {
        return IsGrounded() && jumpElapsedTime > 0;
    }

    bool IsGrounded()
    {
        Ray ray = new Ray();

        ray.origin = transform.position;
        ray.direction = Vector3.down;

        bool hitGround = Physics.Raycast(ray.origin, ray.direction, isGroundedLenght);

        return hitGround;
    }

    #endregion

    #region Voids For Clean Code

    void ManageElapsedTime(ref float _timeToManager)
    {
        if (_timeToManager > 0)
        {
            _timeToManager -= Time.deltaTime;
        }
        else
        {
            _timeToManager = 0;
        }
    }

    #endregion 

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Ray ray = new Ray();

        ray.origin = transform.position;
        ray.direction = Vector3.down;

        Gizmos.DrawLine(ray.origin, (ray.direction * isGroundedLenght) + transform.position);

    }

}
