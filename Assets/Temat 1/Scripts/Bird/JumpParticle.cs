using System;
using UnityEngine;

public class JumpParticle : PoolObject
{
    [SerializeField] private ParticleSystem m_particleSystem;

    private Action m_onFinished;

    public void SetReturnCallback(Action onFinished) => m_onFinished = onFinished;

    protected override void OnActivate()
    {
        m_particleSystem.Play();
    }

    protected override void OnDeactivate()
    {
        m_particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private void Update()
    {
        if (!IsActive)
        {
            return;
        }

        float speed = GameManagerData.Get.PipeScrollSpeed * GameManager.Get.GameSpeed;
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (!m_particleSystem.isPlaying)
        {
            m_onFinished?.Invoke();
        }
    }
}
