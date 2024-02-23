using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheerManager : MonoBehaviour
{
    [SerializeField] List<Animator> _cheerers;
    private readonly int _cheeringTrigger = Animator.StringToHash("Score");

    public static CheerManager Instance { get; private set; }


    private void OnValidate()
    {
        Animator[] allAnimators = FindObjectsOfType<Animator>();
        _cheerers = allAnimators.Where(animator => animator.transform.IsChildOf(transform)).ToList();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void ActivateCheer()
    {
        foreach (var cheerer in _cheerers)
        {
            cheerer?.SetTrigger(_cheeringTrigger);
        }
    }
}
