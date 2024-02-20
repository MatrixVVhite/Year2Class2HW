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

    private void Awake()
    {
        if (ThrowObjectEvent != null)
            ThrowObjectEvent.AddListener(ThrowObject);
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
            throwableObjectRef = gameObject;
    }

    void SendThrowAction()
    {
        //Add a way to figure out which object to throw using throwableObjectRef
        ThrowObjectEvent.Invoke(); //Throw this event only to the object we need
    }
}
