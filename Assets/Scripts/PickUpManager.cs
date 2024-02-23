using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpManager : MonoBehaviour
{
    private GameObject throwableObjectGO;
    private ThrowableObject throwableObject;
    [SerializeField] private PickUpScript pickUpScript;
    public event UnityAction ThrowObject;
    private UnityEvent ThrowObjectEvent = new();
    public event UnityAction PickUpCube;
    private UnityEvent PickUpCubeEvent = new();
    public event UnityAction<ThrowCountData> OnThrow;

    [SerializeField] private int _throwAmount;

    void HandleThrowCount(ThrowCountData data)
    {
        if (_throwAmount < data.HighestThrowCount)
        {
            _throwAmount = data.HighestThrowCount;
            Debug.Log(_throwAmount);
        }
    }

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
        OnThrow += HandleThrowCount;
    }

    private void OnDisable()
    {
        pickUpScript.OnPlayerPickUp -= AcceptGameObject;
        pickUpScript.OnPlayerThrow -= SendThrowAction;
        OnThrow -= HandleThrowCount;
    }

    void AcceptGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            throwableObjectGO = gameObject;
            throwableObject = ThrowableObjectCollection.Instance.GetObject(throwableObjectGO);
            throwableObject.FreezeMyRB();
        }
    }

    void SendThrowAction()
    {
        throwableObject.ThrowMe();
        OnThrow.Invoke(throwableObject.NewThrowData());
        throwableObject = null;
    }
}
