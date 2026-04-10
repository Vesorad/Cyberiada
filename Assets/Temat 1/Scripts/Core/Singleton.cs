using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T Instance;

    public static T Get
    {
        get
        {
            return Instance;
        }
    }

    protected virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"[Singleton] Duplikat {typeof(T).Name} — niszczę.");
            Destroy(gameObject);
            return;
        }

        Instance = this as T;
    }

}
