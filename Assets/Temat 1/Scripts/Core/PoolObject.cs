using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public bool IsActive { get; private set; }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        IsActive = true;
        gameObject.SetActive(true);
        OnActivate();
    }

    public void Deactivate()
    {
        IsActive = false;
        gameObject.SetActive(false);
        OnDeactivate();
    }

    protected virtual void OnActivate() { }
    protected virtual void OnDeactivate() { }
}
