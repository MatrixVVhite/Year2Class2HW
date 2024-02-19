using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public event UnityAction<float> onCharacterVelocityChange;
    [SerializeField] Rigidbody _rb;
    [SerializeField] private float _movementSpeedMultiplier = 100f;
	private UnityEvent<float> onCharacterVelocityChangeEvent = new();
	private Vector2 _move;
	private float _previousCharacterVelocity = 0f;

	private void Awake()
	{
		if (onCharacterVelocityChangeEvent != null)
			onCharacterVelocityChangeEvent.AddListener(onCharacterVelocityChange);
	}

	public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

	private void FixedUpdate()
	{
        _rb.AddForce(new Vector3(_move.x, 0, _move.y) * _movementSpeedMultiplier, ForceMode.Impulse);
        float currentCharacterVelocity = _rb.velocity.magnitude;
		if (currentCharacterVelocity != _previousCharacterVelocity)
            onCharacterVelocityChangeEvent.Invoke(currentCharacterVelocity);
        _previousCharacterVelocity = currentCharacterVelocity;
	}
}
