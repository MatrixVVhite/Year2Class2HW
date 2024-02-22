using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    [SerializeField] private PickUpManager pickUpManager;
    [SerializeField] private Rigidbody _rb;
    private Quaternion _direction;

    private void OnEnable()
    {
        pickUpManager.ThrowObject += ThrowMe;
        pickUpManager.PickUpCube += FreezeMyRB;
    }

    private void OnDisable()
    {
        pickUpManager.ThrowObject -= ThrowMe;
        pickUpManager.PickUpCube -= FreezeMyRB;
    }

    public void FreezeMyRB()
    {
        _rb.constraints = RigidbodyConstraints.FreezePosition;
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.position = transform.parent.position;
    }

    public void ThrowMe()
    {
        _rb.constraints = RigidbodyConstraints.None;
        _direction = transform.parent.rotation;
        Vector3 force = _direction * Vector3.forward * 25;
        _rb.AddForce(force, ForceMode.Impulse);
    }
}
