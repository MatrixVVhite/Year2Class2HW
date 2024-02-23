using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpManager : MonoBehaviour
{
    private GameObject _throwableObjectGO;
    private ThrowableObject _throwableObject;
    [SerializeField] private PickUpScript _pickUpScript;
    public event UnityAction ThrowObject;
    private UnityEvent ThrowObjectEvent = new();
    public event UnityAction PickUpCube;
    private UnityEvent PickUpCubeEvent = new();
    public event UnityAction<ThrowCountData> OnThrow;

    private int _throwScore;

    void HandleThrowCount(ThrowCountData data)
    {
        if (_throwScore < data.HighestThrowCount)
        {
            _throwScore = data.HighestThrowCount;
            CheerManager.Instance.ActivateCheer();
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
        _pickUpScript.OnPlayerPickUp += AcceptGameObject;
        _pickUpScript.OnPlayerThrow += SendThrowAction;
        OnThrow += HandleThrowCount;
    }

    private void OnDisable()
    {
        _pickUpScript.OnPlayerPickUp -= AcceptGameObject;
        _pickUpScript.OnPlayerThrow -= SendThrowAction;
        OnThrow -= HandleThrowCount;
    }

    void AcceptGameObject(GameObject gameObject)
    {
        if (gameObject != null)
        {
            _throwableObjectGO = gameObject;
            _throwableObject = ThrowableObjectCollection.Instance.GetObject(_throwableObjectGO);
            _throwableObject.FreezeMyRB();
        }
    }

    void SendThrowAction()
    {
        _throwableObject.ThrowMe();
        OnThrow.Invoke(_throwableObject.NewThrowData());
        _throwableObject = null;
    }
}
