using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    private readonly int _characterSpeed = Animator.StringToHash("CharacterSpeed");

	private void OnEnable()
	{
        _characterController.onCharacterVelocityChange += UpdateCharacterSpeed;
	}

	private void OnDisable()
	{
		_characterController.onCharacterVelocityChange -= UpdateCharacterSpeed;
	}

	void UpdateCharacterSpeed(float speed)
    {
        _animator.SetFloat(_characterSpeed, speed);
    }
}
