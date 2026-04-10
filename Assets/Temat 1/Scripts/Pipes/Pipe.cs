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
        }
        else
        {
            m_topPipe.localPosition = m_topPipeOriginalLocalPos;
            m_bottomPipe.localPosition = m_bottomPipeOriginalLocalPos;
        }
    }

    protected override void OnDeactivate()
    {
        StopAllCoroutines();
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
