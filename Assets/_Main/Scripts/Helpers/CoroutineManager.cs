using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;

    public static CoroutineManager Instance
    {
        get
        {
            if (_instance != null) return _instance;
            var obj = new GameObject("Coroutine Manager");
            _instance = obj.AddComponent<CoroutineManager>();
            return _instance;
        }
    }

    private readonly Dictionary<string, IEnumerator> _pool = new Dictionary<string, IEnumerator>();

    public void RegisterListener(string actionName, IEnumerator coroutine)
    {
        if (_pool.TryAdd(actionName, coroutine)) return;
        Debug.Log($"Contained key coroutine: {actionName}");
        return;
    }

    public void RemoveListener(string actionName)
    {
        if (!_pool.ContainsKey(actionName))
        {
            Debug.Log($"No contained key coroutine: {actionName}");
            return;
        }
        _pool.Remove(actionName);
    }

    public void TriggerListener(string actionName)
    {
        if (!_pool.TryGetValue(actionName, value: out var value))
        {
            Debug.Log($"No contained key coroutine: {actionName}");
            Debug.Log($"Can not trigger key: {actionName}");
            return;
        }
        StartCoroutine(value);
    }

    public void StopListener(string actionName)
    {
        if (!_pool.TryGetValue(actionName, out var value))
        {
            Debug.Log($"No contained key coroutine: {actionName}");
            Debug.Log($"Can not stop key: {actionName}");
            return;
        }
        StopCoroutine(value);
    }
}