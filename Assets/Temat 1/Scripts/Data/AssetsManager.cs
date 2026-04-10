using UnityEngine;

[CreateAssetMenu(fileName = "AssetsManager", menuName = "FlappyBird/AssetsManager")]
public class AssetsManager : SingletonScriptableObject<AssetsManager>
{
    [field: Header("Pipes")]
    [field: SerializeField] public Pipe PipePrefab { get; private set; }

    [field: Header("Bird")]
    [field: SerializeField] public Animator BirdBodyPrefab { get; private set; }
}
