using System.Collections.Generic;
using UnityEngine;

public class GetComponentBenchmarkSpawner : MonoBehaviour
{
    public enum Mode { Inefficient, Efficient }

    [Header("Konfiguracja")]
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private Mode m_mode = Mode.Inefficient;
    [SerializeField] private int m_objectCount = 500;
    [SerializeField] private Color m_color = Color.red;

    private readonly List<InefficientGetComponentDemo> m_inefficientObjects = new();
    private readonly List<EfficientGetComponentDemo> m_efficientObjects = new();
    private Color m_previousColor;

    private void Start()
    {
        var cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        var spawnArea = new Vector2(height * cam.aspect, height);

        for (int i = 0; i < m_objectCount; i++)
        {
            float x = Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f);
            float y = Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f);

            GameObject go;
            if (m_prefab != null)
            {
                go = Instantiate(m_prefab, new Vector3(x, y, 0f), Quaternion.identity);
            }
            else
            {
                go = GameObject.CreatePrimitive(PrimitiveType.Cube);
                go.transform.position = new Vector3(x, y, 0f);
                go.transform.localScale = Vector3.one * 0.3f;
                Destroy(go.GetComponent<BoxCollider>());
            }
            go.name = $"Obj_{i}";

            if (m_mode == Mode.Inefficient)
            {
                var comp = go.GetComponent<InefficientGetComponentDemo>() ?? go.AddComponent<InefficientGetComponentDemo>();
                comp.SetColor(m_color);
                m_inefficientObjects.Add(comp);
            }
            else
            {
                var comp = go.GetComponent<EfficientGetComponentDemo>() ?? go.AddComponent<EfficientGetComponentDemo>();
                comp.SetColor(m_color);
                m_efficientObjects.Add(comp);
            }
        }

        m_previousColor = m_color;
    }

    private void Update()
    {
        if (m_color == m_previousColor)
        {
            return;
        }

        foreach (var obj in m_inefficientObjects)
        {
            obj.SetColor(m_color);
        }

        foreach (var obj in m_efficientObjects)
        {
            obj.SetColor(m_color);
        }

        m_previousColor = m_color;
    }
}
