using UnityEngine;

[CreateAssetMenu(fileName = "AssetsManager", menuName = "FlappyBird/AssetsManager")]
public class AssetsManager : SingletonScriptableObject<AssetsManager>
{
    [field: Header("Pipes")]
    [field: SerializeField] public Pipe PipePrefab { get; private set; }

}
