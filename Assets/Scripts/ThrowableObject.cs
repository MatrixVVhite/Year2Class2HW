using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowableObject : MonoBehaviour
{

    [SerializeField] private PickUpManager _pickUpManager;
    [SerializeField] private Rigidbody _rb;
    private Quaternion _direction;
    private int _throwCount;

    private void OnValidate()
    {
        this.tag = "Cube";
    }

    private void OnEnable()
    {
        _pickUpManager.ThrowObject += ThrowMe;
        _pickUpManager.PickUpCube += FreezeMyRB;
    }

    private void OnDisable()
    {
        _pickUpManager.ThrowObject -= ThrowMe;
        _pickUpManager.PickUpCube -= FreezeMyRB;
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

    public ThrowCountData NewThrowData()
    {
        return new ThrowCountData(++_throwCount);
    }
}
