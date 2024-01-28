using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUpObjectComponent : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private Collider modelColl;
    private void Awake()
    {
        if(rb == null )
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public void SetParent(Transform _parent)
    {
        modelColl.enabled = false;
        rb.useGravity = false;

        this.transform.parent = _parent;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        this.transform.localPosition = Vector3.zero + Vector3.up * transform.localScale.y / 2;
    }

    public void Throw(Vector3 _playerForward)
    {
        this.transform.parent = null;
        rb.useGravity = true;

        Vector3 throwDir = _playerForward;

        rb.AddForce(throwDir.normalized * 10, ForceMode.Impulse);

        modelColl.enabled = true;
    }
}
