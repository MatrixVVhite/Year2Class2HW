using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public event UnityAction<float> onCharacterVelocityChange;
	public event UnityAction<Quaternion> onCharacterDirectionChange;
	[SerializeField] Rigidbody _rb;
	[SerializeField] Transform _movementDirection;
	[SerializeField] float _walkVelocity = 100f;
	[SerializeField] float _sprintVelocity = 200f;
	private float _movementVelocityMultiplier;
	private UnityEvent<float> onCharacterVelocityChangeEvent = new();
	private UnityEvent<Quaternion> onCharacterDirectionChangeEvent = new();
	private Vector2 _move;
	private Vector3 _previousCharacterPosition;
	private float _previousCharacterVelocity;
	private Quaternion _previousCharacterDirection;
	private Quaternion _currentCharacterDirection;

	private float CurrentCharacterVelocity => (_previousCharacterPosition - _rb.position).magnitude/Time.fixedDeltaTime;

	private void Awake()
	{
		_movementVelocityMultiplier = _walkVelocity;
		if (onCharacterVelocityChangeEvent != null)
			onCharacterVelocityChangeEvent.AddListener(onCharacterVelocityChange);
		if (onCharacterDirectionChangeEvent != null)
			onCharacterDirectionChangeEvent.AddListener(onCharacterDirectionChange);
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
		UpdateMovement();
		UpdateVelocity();
		UpdateRotation();
		UpdatePreviousValues();
	}

	private void UpdateMovement()
	{
		float velocity = _move.magnitude * _movementVelocityMultiplier;
		_currentCharacterDirection = Quaternion.Euler(0, Vector2.SignedAngle(_move, Vector2.up), 0);
		_currentCharacterDirection *= Quaternion.Euler(0, _movementDirection.rotation.eulerAngles.y, 0);
		Vector3 force = _currentCharacterDirection * Vector3.forward * velocity;
		_rb.AddForce(force, ForceMode.Impulse);
	}

	private void UpdateVelocity()
	{
		if (CurrentCharacterVelocity != _previousCharacterVelocity)
			onCharacterVelocityChangeEvent.Invoke(CurrentCharacterVelocity);
	}

	private void UpdateRotation()
	{
		if (_currentCharacterDirection != _previousCharacterDirection)
			onCharacterDirectionChangeEvent.Invoke(_currentCharacterDirection);
	}

	private void UpdatePreviousValues()
	{
		_previousCharacterPosition = _rb.position;
		_previousCharacterVelocity = CurrentCharacterVelocity;
		_previousCharacterDirection = _currentCharacterDirection;
	}
}
