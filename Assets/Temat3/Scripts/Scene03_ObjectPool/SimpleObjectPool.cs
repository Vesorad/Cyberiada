using System.Collections.Generic;
using UnityEngine;

public class SimpleObjectPool<T> where T : MonoBehaviour
{
    private readonly T m_prefab;
    private readonly Transform m_parent;
    private readonly Queue<T> m_available = new Queue<T>();

    public SimpleObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        m_prefab = prefab;
        m_parent = parent;

        for (int i = 0; i < initialSize; i++)
        {
            T obj = Object.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            m_available.Enqueue(obj);
        }
    }

    public T Get(Vector3 position)
    {
        T obj;

        if (m_available.Count > 0)
        {
            obj = m_available.Dequeue();
        }
        else
        {
            obj = Object.Instantiate(m_prefab, m_parent);
        }

        obj.transform.position = position;
        return obj;
    }

    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        m_available.Enqueue(obj);
    }
}
