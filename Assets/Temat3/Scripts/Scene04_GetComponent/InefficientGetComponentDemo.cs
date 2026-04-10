using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class InefficientGetComponentDemo : MonoBehaviour
{
    private Color m_targetColor = Color.red;

    public void SetColor(Color color) => m_targetColor = color;

    private void Update()
    {
        for (int i = 0; i < 50; i++)
        {
            GetComponent<Renderer>().material.color = m_targetColor;
        }
    }
}
