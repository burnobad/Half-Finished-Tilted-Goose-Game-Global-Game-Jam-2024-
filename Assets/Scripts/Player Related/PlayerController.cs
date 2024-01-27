using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Components
    [Header("Components")]

    [SerializeField]
    private Rigidbody rb;

    #endregion

    #region Serializeble Variables
    [Header("Serializeble Variables")]

    [SerializeField, Range(1, 8)]
    private float moveSpeed;

    [SerializeField, Range(1, 8)]
    private float jumpForce;

    [SerializeField, Range(0.01f, 5)]
    private float isGroundedLenght;

    #endregion

    #region Private Variables

    private Vector3 inputDir;
    private Vector3 moveDir;
    #endregion

    void Update()
    {
        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space))
        {
            inputDir.y = 1;
        }

        moveDir = new Vector3(inputDir.x, 0, inputDir.z);
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

    }

    #region Calculated Variable and Checks

    bool CanJump()
    {
        return IsGrounded() && inputDir.y != 0;
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Ray ray = new Ray();

        ray.origin = transform.position;
        ray.direction = Vector3.down;

        Gizmos.DrawLine(ray.origin, (ray.direction * isGroundedLenght) + transform.position);

    }

}
