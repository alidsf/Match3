using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolBase<T> where T : PooledObject<T>
{
    private T _prefab;
    private Transform _parent;
    private Func<T, Transform, T> _instantiate;
    private Action<GameObject> _destroy;

    private IObjectPool<T> _pool;
    private IObjectPool<T> pool
    {
        get
        {
            if (_pool == null)
                _pool = new ObjectPool<T>(OnCreateObject, OnGetObject,
                    OnReleaseObject, OnDestroyObject);

            return _pool;
        }
    }

    public void Init(T prefab, Transform parent, Func<T, Transform, T> instantiate, Action<GameObject> destroy)
    {
        _prefab = prefab;
        _parent = parent;
        _instantiate = instantiate;
        _destroy = destroy;
    }

    private T OnCreateObject()
    {
        T blastParticle = _instantiate(_prefab, _parent);
        blastParticle.Init(pool.Release);
        return blastParticle;
    }

    private void OnGetObject(T blastParticle) =>
        blastParticle.gameObject.SetActive(true);

    private void OnReleaseObject(T blastParticle) =>
        blastParticle.gameObject.SetActive(false);

    private void OnDestroyObject(T blastParticle) =>
        _destroy(blastParticle.gameObject);

    public T GetNewObject()
    {
        return pool.Get();
    }
}

public class PooledObject<T> : MonoBehaviour
{
    protected Action<T> _release;
    public void Init(Action<T> release) =>
        _release = release;
}
