using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    private readonly int _characterSpeed = Animator.StringToHash("CharacterSpeed");

	private void OnEnable()
	{
        _characterController.onCharacterVelocityChange += UpdateCharacterSpeed;
		_characterController.onCharacterDirectionChange += UpdateCharacterRotation;
	}

	private void OnDisable()
	{
		_characterController.onCharacterVelocityChange -= UpdateCharacterSpeed;
		_characterController.onCharacterDirectionChange -= UpdateCharacterRotation;
	}

	void UpdateCharacterSpeed(float speed)
    {
        _animator.SetFloat(_characterSpeed, speed);
    }

	void UpdateCharacterRotation(Vector2 rotation)
	{
		if (rotation != Vector2.zero)
			transform.rotation = Quaternion.Euler(0, Vector2.SignedAngle(rotation, Vector2.up), 0);
	}
}
