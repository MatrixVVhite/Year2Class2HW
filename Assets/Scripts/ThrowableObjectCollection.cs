using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThrowableObjectCollection : MonoBehaviour
{
    [SerializeField] private List<ThrowableObject> list;
    private Dictionary<GameObject, ThrowableObject> dictionary;

    public static ThrowableObjectCollection Instance { get; private set; }

    private void OnValidate()
    {
        list = FindObjectsOfType<ThrowableObject>().ToList();
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        dictionary = new Dictionary<GameObject, ThrowableObject>(list.Count);
        foreach (var throwableObject in list)
        {
            dictionary.Add(throwableObject.gameObject, throwableObject);
        }
    }

    public ThrowableObject GetObject(GameObject obj)
    {
        return dictionary[obj];
    }
}
