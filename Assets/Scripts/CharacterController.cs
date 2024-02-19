using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    public UnityEvent<float> onCharacterVelocityChange;
    [SerializeField] Rigidbody _rb;
    [SerializeField] private float _movementSpeedMultiplier = 100f;
	private Vector2 _move;
	private float _previousCharacterVelocity = 0f;

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

	private void FixedUpdate()
	{
        _rb.AddForce(new Vector3(_move.x, 0, _move.y) * _movementSpeedMultiplier);
        float currentCharacterVelocity = _rb.velocity.magnitude;
		if (currentCharacterVelocity != _previousCharacterVelocity)
            onCharacterVelocityChange.Invoke(currentCharacterVelocity);
        _previousCharacterVelocity = currentCharacterVelocity;
	}
}
