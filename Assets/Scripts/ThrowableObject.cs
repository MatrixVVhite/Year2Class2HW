using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    [SerializeField] private PickUpManager pickUpManager;
    [SerializeField] private GameObject originPoint;
    [SerializeField] private Rigidbody _rb;
    private Quaternion _direction;

    private void OnEnable()
    {
        pickUpManager.ThrowObject += ThrowMe;
    }

    private void OnDisable()
    {
        pickUpManager.ThrowObject -= ThrowMe;
    }

    void ThrowMe()
    {
        //Update _direction to camera's direction
        Transform throwOriginPoint = originPoint.transform;
        //Add 2 forward depending on _direction
        Vector3 force = _direction * Vector3.forward * 100;
        _rb.AddForce(force, ForceMode.Impulse);
    }
}
