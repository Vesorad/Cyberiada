using UnityEngine;

public class DrawCallsSpawner : MonoBehaviour
{
    public enum BatchingMode
    {
        NoBatching,
        Batching
    }

    [Header("Konfiguracja")]
    [SerializeField] private int m_columns = 50;
    [SerializeField] private int m_rows = 40;
    [SerializeField] private float m_spacing = 1.1f;
    [SerializeField] private BatchingMode m_mode = BatchingMode.NoBatching;
    [SerializeField] private Material m_material = null;

    private Sprite m_sharedSprite;

    private void Start()
    {
        var sharedTex = CreateTexture(Color.white);
        m_sharedSprite = Sprite.Create(sharedTex, new Rect(0, 0, 32, 32), Vector2.one * 0.5f, 32f);

        Vector3 offset = new Vector3(
            -m_columns * m_spacing / 2f,
            -m_rows * m_spacing / 2f,
            0f);

        for (int row = 0; row < m_rows; row++)
        {
            for (int col = 0; col < m_columns; col++)
            {
                SpawnObject(col, row, offset);
            }
        }
    }

    private void SpawnObject(int col, int row, Vector3 offset)
    {
        var go = new GameObject($"Sprite_{col}_{row}");
        go.transform.position = new Vector3(col * m_spacing, row * m_spacing, 0f) + offset;

        var sr = go.AddComponent<SpriteRenderer>();

        if (m_mode == BatchingMode.NoBatching)
        {
            var tex = CreateTexture(Random.ColorHSV());
            sr.sprite = Sprite.Create(tex, new Rect(0, 0, 32, 32), Vector2.one * 0.5f, 32f);
        }
        else
        {
            sr.material = m_material;
            sr.sprite = m_sharedSprite;
        }
    }

    private static Texture2D CreateTexture(Color color)
    {
        var tex = new Texture2D(32, 32);
        var pixels = new Color[32 * 32];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = color;
        }
        tex.SetPixels(pixels);
        tex.Apply();
        return tex;
    }
}
