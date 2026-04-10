using UnityEngine;

public class PhysicsOverheadSpawner : MonoBehaviour
{
    public enum PhysicsMode { NoRigidbody, KinematicRigidbody }

    [Header("Konfiguracja")]
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private PhysicsMode m_mode = PhysicsMode.NoRigidbody;
    [SerializeField] private int m_objectCount = 2000;

    private Vector2 m_spawnArea;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        var cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        m_spawnArea = new Vector2(height * cam.aspect, height);

        for (int i = 0; i < m_objectCount; i++)
        {
            SpawnObject(i);
        }
    }

    private void SpawnObject(int index)
    {
        float x = Random.Range(-m_spawnArea.x / 2f, m_spawnArea.x / 2f);
        float y = Random.Range(-m_spawnArea.y / 2f, m_spawnArea.y / 2f);

        var go = m_prefab != null
            ? Instantiate(m_prefab, new Vector3(x, y, 0f), Quaternion.identity)
            : new GameObject($"PhysicsObj_{index}");
        go.name = $"PhysicsObj_{index}";
        go.transform.position = new Vector3(x, y, 0f);

        if (go.GetComponent<BoxCollider2D>() == null)
        {
            go.AddComponent<BoxCollider2D>();
        }

        if (go.GetComponent<MovingPhysicsObject>() == null)
        {
            go.AddComponent<MovingPhysicsObject>();
        }

        if (m_mode == PhysicsMode.KinematicRigidbody)
        {
            if (!go.TryGetComponent(out Rigidbody2D rb))
            {
                rb = go.AddComponent<Rigidbody2D>();
            }

            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.gravityScale = 0f;
        }
    }
}
