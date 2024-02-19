using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public event UnityAction<float> onCharacterVelocityChange;
    [SerializeField] Rigidbody _rb;
	[SerializeField] float _walkVelocity = 100f;
	[SerializeField] float _sprintVelocity = 200f;
	private float _movementVelocityMultiplier;
	private UnityEvent<float> onCharacterVelocityChangeEvent = new();
	private Vector2 _move;
	private Vector3 _previousCharacterPosition;
	private float _previousCharacterVelocity;

	private float CurrentCharacterVelocity => (_previousCharacterPosition - _rb.position).magnitude/Time.fixedDeltaTime;

	private void Awake()
	{
		_movementVelocityMultiplier = _walkVelocity;
		if (onCharacterVelocityChangeEvent != null)
			onCharacterVelocityChangeEvent.AddListener(onCharacterVelocityChange);
	}

	public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

	public void OnSprint(InputAction.CallbackContext context)
	{
		if (context.started)
			_movementVelocityMultiplier = _sprintVelocity;
		if (context.canceled)
			_movementVelocityMultiplier = _walkVelocity;
	}

	private void FixedUpdate()
	{
        _rb.AddForce(new Vector3(_move.x, 0, _move.y) * _movementVelocityMultiplier, ForceMode.Impulse);
		if (CurrentCharacterVelocity != _previousCharacterVelocity)
            onCharacterVelocityChangeEvent.Invoke(CurrentCharacterVelocity);
        _previousCharacterPosition = _rb.position;
		_previousCharacterVelocity = CurrentCharacterVelocity;
	}
}
