using System.Collections;
using UnityEngine;

public class Pipe : PoolObject
{
    [SerializeField] private Transform m_topPipe;
    [SerializeField] private Transform m_bottomPipe;
    [SerializeField] private ScoreZone m_scoreZone;

    public float GapCenterY => m_scoreZone.transform.position.y;
    public float GapCenterX => m_scoreZone.transform.position.x;
    private Vector3 m_topPipeOriginalLocalPos;
    private Vector3 m_bottomPipeOriginalLocalPos;

    private void Awake()
    {
        m_topPipeOriginalLocalPos = m_topPipe.localPosition;
        m_bottomPipeOriginalLocalPos = m_bottomPipe.localPosition;
    }

    protected override void OnActivate()
    {
        m_scoreZone.Reset();

        if (GameManagerData.Get.PipeSlideInEnabled)
        {
            StartCoroutine(PipeSlide());
        }
        else
        {
            m_topPipe.localPosition = m_topPipeOriginalLocalPos;
            m_bottomPipe.localPosition = m_bottomPipeOriginalLocalPos;
        }
    }

    protected override void OnDeactivate()
    {

    }

    private IEnumerator PipeSlide()
    {
        float halfScreen = Camera.main.orthographicSize;
        float duration = GameManagerData.Get.PipeSlideInTime;

        Vector3 topFrom = m_topPipeOriginalLocalPos + Vector3.up * halfScreen;
        Vector3 bottomFrom = m_bottomPipeOriginalLocalPos + Vector3.down * halfScreen;

        m_topPipe.localPosition = topFrom;
        m_bottomPipe.localPosition = bottomFrom;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            m_topPipe.localPosition = Vector3.Lerp(topFrom, m_topPipeOriginalLocalPos, t);
            m_bottomPipe.localPosition = Vector3.Lerp(bottomFrom, m_bottomPipeOriginalLocalPos, t);

            yield return null;
        }
    }
    private void Update()
    {
        if (IsActive == false || GameManager.Get.CurrentState != GameState.Playing)
        {
            return;
        }

        Scroll();
        CheckDestroy();
    }

    private void Scroll()
    {
        transform.Translate(Vector3.left * GameManagerData.Get.PipeScrollSpeed * GameManager.Get.GameSpeed * Time.deltaTime);
    }

    private void CheckDestroy()
    {
        if (transform.position.x <= GameManagerData.Get.PipeDestroyX)
        {
            GameManager.Get.ReturnPipe(this);
        }
    }

}
