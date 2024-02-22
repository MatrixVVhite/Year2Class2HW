using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpManager : MonoBehaviour
{
    private GameObject throwableObjectRef;
    [SerializeField] private PickUpScript pickUpScript;
    public event UnityAction ThrowObject;
    private UnityEvent ThrowObjectEvent = new();
    public event UnityAction PickUpCube;
    private UnityEvent PickUpCubeEvent = new();

    private void Awake()
    {
        if (ThrowObjectEvent != null)
            ThrowObjectEvent.AddListener(ThrowObject);
        if (PickUpCubeEvent != null)
            PickUpCubeEvent.AddListener(PickUpCube);
    }

    private void OnEnable()
    {
        pickUpScript.OnPlayerPickUp += AcceptGameObject;
        pickUpScript.OnPlayerThrow += SendThrowAction;
    }

    private void OnDisable()
    {
        pickUpScript.OnPlayerPickUp -= AcceptGameObject;
        pickUpScript.OnPlayerThrow -= SendThrowAction;
    }

    void AcceptGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            throwableObjectRef = gameObject;
            throwableObjectRef.GetComponent<ThrowableObject>().FreezeMyRB();
        }
    }

    void SendThrowAction()
    {
        throwableObjectRef.GetComponent<ThrowableObject>().ThrowMe();
    }
}
