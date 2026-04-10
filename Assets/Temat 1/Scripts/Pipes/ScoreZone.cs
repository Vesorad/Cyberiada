using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    [SerializeField] private Collider2D m_collider;

    public void Reset()
    {
        m_collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Constants.Tags.Player) == false)
        {
            return;
        }

        m_collider.enabled = false;
        GameManager.Get.AddPoint();
    }
}
