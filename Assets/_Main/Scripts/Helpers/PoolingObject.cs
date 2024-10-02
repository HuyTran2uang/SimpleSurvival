using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class PoolingObject
{
    [SerializeField] private GameObject _prefab;

    public Queue<GameObject> Pool { get; } = new Queue<GameObject>();

    public List<GameObject> Using { get; } = new List<GameObject>();

    public PoolingObject() { }

    public PoolingObject(GameObject prefab, int initialSize = 0)
    {
        _prefab = prefab;

        for (var i = 0; i < initialSize; i++)
        {
            var obj = CreateNew();
            obj.gameObject.SetActive(false);
            Pool.Enqueue(obj);
        }
    }

    private GameObject CreateNew()
    {
        return Object.Instantiate(_prefab);
    }

    public GameObject Get()
    {
        var obj = Pool.Count > 0 ? Pool.Dequeue() : CreateNew();

        Using.Add(obj);

        return obj;
    }

    public void Return(GameObject obj)
    {
        Using.Remove(obj);
        Pool.Enqueue(obj);
    }

    public void Clear()
    {
        Using.ForEach(i => Pool.Enqueue(i));
        Using.Clear();
    }

    public void Destroy()
    {
        Using.ForEach(Object.Destroy);
        Pool.ToList().ForEach(Object.Destroy);
        Using.Clear();
        Pool.Clear();
    }
}

[System.Serializable]
public class PoolingObject<T>
    where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    private readonly Queue<T> _pool = new Queue<T>();
    private readonly List<T> _using = new List<T>();

    public Queue<T> Pool => _pool;
    public List<T> Using => _using;

    public PoolingObject() { }

    public PoolingObject(T prefab, int initialSize = 0)
    {
        _prefab = prefab;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = CreateNew();
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    private T CreateNew()
    {
        return Object.Instantiate(_prefab);
    }

    public T Get()
    {
        T obj = null;

        if (_pool.Count > 0)
        {
            obj = _pool.Dequeue();
            _using.Add(obj);
        }
        else
        {
            obj = CreateNew();
            _using.Add(obj);
        }

        return obj;
    }

    public void Return(T obj)
    {
        _using.Remove(obj);
        _pool.Enqueue(obj);
    }

    public void Clear()
    {
        _using.ForEach(i => _pool.Enqueue(i));
        _using.Clear();
    }

    public void Destroy()
    {
        _using.ForEach(i => Object.Destroy(i));
        _pool.ToList().ForEach(i => Object.Destroy(i));
        _using.Clear();
        _pool.Clear();
    }
}