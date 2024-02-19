using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform CameraTransform;
    private Vector3 _rotation;

    private void Awake()
    {
        _rotation = CameraTransform.rotation.eulerAngles;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 rotate = context.ReadValue<Vector2>();
        Vector3 rotationDelta = new(-rotate.y, rotate.x, 0);
        float x = Mathf.Clamp(_rotation.x + rotationDelta.x, -85, 85);
        _rotation = new(x, _rotation.y + rotationDelta.y, 0);
        CameraTransform.rotation = Quaternion.Euler(_rotation);
    }
}
