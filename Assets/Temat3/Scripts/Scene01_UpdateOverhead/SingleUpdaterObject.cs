using UnityEngine;

public class SingleUpdaterObject : MonoBehaviour
{
    private const int IterationCount = 10000;

    private void Update()
    {
        for (int i = 0; i < IterationCount; i++)
        {
            DoWork();
        }
    }

    private void DoWork()
    {
        float _ = Mathf.Sin(Time.time);
        Debug.Log(_);
    }
}
