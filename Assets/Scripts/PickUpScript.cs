using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PickUpScript : MonoBehaviour
{
    [SerializeField] private PickUpManager pickUpManager;
    [SerializeField] private GameObject originPoint;
    public event UnityAction<GameObject> OnPlayerPickUp;
    private UnityEvent<GameObject> OnPlayerPickUpEvent = new();
    public event UnityAction OnPlayerThrow;
    private UnityEvent OnPlayerThrowEvent = new();

    private GameObject _cubeInHands = null;

    private bool CubeInHand => _cubeInHands != null;

    private void Awake()
    {
        if (OnPlayerPickUpEvent != null)
            OnPlayerPickUpEvent.AddListener(OnPlayerPickUp);
        if (OnPlayerThrowEvent != null)
            OnPlayerThrowEvent.AddListener(OnPlayerThrow);
    }

    private void OnEnable()
    {
        pickUpManager.ThrowObject += DropCube;
    }

    private void OnDisable()
    {
        pickUpManager.ThrowObject -= DropCube;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!CubeInHand)
                OnPlayerPickUp.Invoke(TryPickUp());
            else
                OnPlayerThrow.Invoke();
        } 
    }

    GameObject TryPickUp()
    {
        Ray lookingAt = new Ray(originPoint.transform.position, originPoint.transform.forward);

        if (!CubeInHand)
        {
            if (Physics.Raycast(lookingAt, out RaycastHit hit, 20))
            {
                if (!hit.IsUnityNull())
                {
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.tag == "Cube")
                    {
                        Debug.Log("Cube detected");
                        _cubeInHands = hit.collider.gameObject;
                        return _cubeInHands;
                    }
                }
            }
        }
        return null;
    }

    void DropCube()
    {
        _cubeInHands = null;
    }
}
