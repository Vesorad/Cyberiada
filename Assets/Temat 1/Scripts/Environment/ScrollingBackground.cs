using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    [SerializeField] private float m_scrollScale = Constants.Background.ScrollScale;
    private Material m_material;
    private float m_offset;

    private void Reset()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Awake()
    {
        m_material = m_spriteRenderer.material;
    }

    private void Update()
    {
        if (GameManager.Get.CurrentState != GameState.Playing)
        {
            return;
        }

        Scroll();
    }

    private void Scroll()
    {

        float speed = GameManagerData.Get.PipeScrollSpeed * GameManager.Get.GameSpeed;
        m_offset = (m_offset + speed * m_scrollScale * Time.deltaTime) % 1f;
        m_material.mainTextureOffset = new Vector2(m_offset, 0f);
    }
}
