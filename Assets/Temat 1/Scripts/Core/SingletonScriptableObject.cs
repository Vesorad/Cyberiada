using UnityEngine;

public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T Instance;

    public static T Get
    {
        get
        {
            if (Instance == null)
            {
                Instance = Resources.Load<T>(typeof(T).Name);

                if (Instance == null)
                {
                    Debug.LogError($"[SingletonScriptableObject] Brak zasobu '{typeof(T).Name}' w Resources.");
                }
            }

            return Instance;
        }
    }
}
