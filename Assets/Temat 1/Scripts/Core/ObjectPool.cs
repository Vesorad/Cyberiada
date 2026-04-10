using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : PoolObject
{
    private readonly T m_prefab;
    private readonly Transform m_parent;
    private readonly Queue<T> m_pool = new Queue<T>();

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        m_prefab = prefab;
        m_parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            m_pool.Enqueue(CreateInstance(i));
        }
    }

    public T Get(Vector3 position)
    {
        T obj = m_pool.Count > 0 ? m_pool.Dequeue() : CreateInstance(m_pool.Count + 1);
        obj.Activate(position);
        return obj;
    }

    public void Return(T obj)
    {
        obj.Deactivate();
        m_pool.Enqueue(obj);
    }

    private T CreateInstance(int number)
    {
        T obj = Object.Instantiate(m_prefab, m_parent);
        obj.gameObject.name = $"{m_prefab.name}_{number}";
        obj.Deactivate();
        return obj;
    }
}
