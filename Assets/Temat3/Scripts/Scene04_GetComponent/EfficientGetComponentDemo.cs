using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EfficientGetComponentDemo : MonoBehaviour
{
    private Color m_targetColor = Color.green;
    private Renderer m_renderer;

    public void SetColor(Color color) => m_targetColor = color;

    private void Awake()
    {
        m_renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        m_renderer.material.color = m_targetColor;
    }
}
