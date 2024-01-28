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
    private Camera cam;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private LayerMask canPickUp;

    private static PlayerController instance;
    public static PlayerController Instance
    { get { return instance; } }    

    #endregion

    #region Serializeble Variables
    [Header("Serializeble Variables")]

    [SerializeField, Range(100, 800)]
    private float mouseSensX;

    [SerializeField, Range(1, 8)]
    private float moveSpeed;

    [SerializeField, Range(1, 6)]
    private float jumpForce;

    [SerializeField, Range(0.01f, 1)]
    private float isGroundedLenght;

    [SerializeField, Range(0.01f, 5)]
    private float pickUpDistance;

    [SerializeField, Range(1, 3)]
    private int maxHealth;

    #endregion

    #region Private Variables

    private const float BUTTON_PRESS_TIME = 0.23f;

    private float currentHealth;

    private Vector3 inputDir;
    private Vector3 moveDir;

    private float yRotation;

    private float jumpElapsedTime;

    private PickUpObjectComponent currentPickUp;
    private Collider[] pickUpHit;

    bool isDead;
    #endregion

    private void Awake()
    {
        instance = this;

        inputDir = Vector3.zero;
        moveDir = Vector3.zero;

        jumpElapsedTime = 0;

        currentPickUp = null;

        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        currentHealth = maxHealth;
        isDead = false;
    }

    void Update()
    {
        if(isDead)
        {

            if(Input.GetMouseButtonDown(0)) 
            {
                EventsManager_PersistenceScene.ReloadSceneEvent();
            }
        }

        inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpElapsedTime = BUTTON_PRESS_TIME;
        }

        moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;
        moveDir.Normalize();

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensX;

        yRotation += mouseX;

        if(Input.GetMouseButtonDown(0) && !isDead) 
        {
            if(CanPickUp()) 
            {
                currentPickUp = GetClosestPickUp(pickUpHit).GetComponentInParent<PickUpObjectComponent>();

                currentPickUp.SetParent(pickUpParent);
            }
            else if(CanThrow())
            {
                currentPickUp.Throw(transform.forward);
                currentPickUp = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if(!isDead)
        {
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            rb.velocity = new Vector3(moveDir.x * moveSpeed, rb.velocity.y, moveDir.z * moveSpeed);

            if (CanJump())
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }

            ManageElapsedTime(ref jumpElapsedTime);
        }
    }

    

    public void GetDamaged()
    {
        currentHealth--;

        if (currentHealth <= 0)
        {
            Death();
        }

    }
    void Death()
    {
        isDead = true;
        transform.rotation = Quaternion.Euler(0f, 0f, 90f);
    }

    #region Calculated Variable and Checks

    bool CanJump()
    {
        return IsGrounded() && jumpElapsedTime > 0;
    }

    bool CanPickUp()
    {
        return FoundPickUp() && currentPickUp == null;
    }

    bool CanThrow()
    {
        return currentPickUp != null;
    }

    bool IsGrounded()
    {
        Ray ray = new Ray();

        ray.origin = transform.position;
        ray.direction = Vector3.down;

        bool hitGround = Physics.Raycast(ray.origin, ray.direction, isGroundedLenght);

        return hitGround;
    }

    bool FoundPickUp()
    {
        pickUpHit = Physics.OverlapSphere(transform.position + transform.forward * pickUpDistance, pickUpDistance, canPickUp);

        return pickUpHit.Length > 0;
    }

    #endregion

    #region For Clean Code

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

    Transform GetClosestPickUp(Collider[] _collArray)
    {
        Transform pickUp = null;

        float lowestDIst = pickUpDistance * 2;

        foreach (Collider coll in _collArray) 
        {
            float dist = Vector3.Distance(transform.position, coll.transform.position);
            if (dist < lowestDIst)
            {
                lowestDIst = dist;
                pickUp = coll.transform;
            }
        }

        return pickUp;
    }

    #endregion 

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Ray groundCheckRay = new Ray();
        groundCheckRay.origin = transform.position;
        groundCheckRay.direction = Vector3.down;

        Gizmos.DrawLine(groundCheckRay.origin, (groundCheckRay.direction * isGroundedLenght) + transform.position);

        Gizmos.color = Color.green;
  
        Gizmos.DrawWireSphere(transform.position + transform.forward * pickUpDistance, pickUpDistance);

    }

}
