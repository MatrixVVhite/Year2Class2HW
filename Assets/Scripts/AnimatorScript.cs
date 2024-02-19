using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    private readonly int _characterSpeed = Animator.StringToHash("CharacterSpeed");

    void UpdateCharacterSpeed(float speed)
    {
        animator.SetFloat(_characterSpeed, speed);
    }
}
