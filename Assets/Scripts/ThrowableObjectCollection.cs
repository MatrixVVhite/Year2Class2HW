using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowableObjectCollection : MonoBehaviour
{
    [SerializeField] private List<ThrowableObject> _allThrowableObjects;
    private Dictionary<GameObject, ThrowableObject> _allThrowableObjectstionaryAndGO;

    public static ThrowableObjectCollection Instance { get; private set; }

    private void OnValidate()
    {
        _allThrowableObjects = FindObjectsOfType<ThrowableObject>().ToList();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        _allThrowableObjectstionaryAndGO = new Dictionary<GameObject, ThrowableObject>(_allThrowableObjects.Count);
        foreach (var throwableObject in _allThrowableObjects)
        {
            _allThrowableObjectstionaryAndGO.Add(throwableObject.gameObject, throwableObject);
        }
    }

    public ThrowableObject GetObject(GameObject obj)
    {
        return _allThrowableObjectstionaryAndGO[obj];
    }
}
