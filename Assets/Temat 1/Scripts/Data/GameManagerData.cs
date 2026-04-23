using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "FlappyBird/GameManagerData")]
public class GameManagerData : SingletonScriptableObject<GameManagerData>
{
    [field: Header("Game Speed")]
    [field: SerializeField,Range(0,1)] public float SpeedIncreasePerPoint { get; private set; } = 0.05f;
    [field: SerializeField,Range(1,3)] public float MaxGameSpeed { get; private set; } = 3f;

    [field: Header("Bot")]
    [field: SerializeField] public bool BotEnabled { get; private set; }


    [field: Header("Pipes")]
    [field: SerializeField,Range(0,10)] public float PipeScrollSpeed { get; private set; } = 3f;
    [field: SerializeField,Range(3,5)] public float PipeSpawnInterval { get; private set; } = 2f;
    [field: SerializeField,Range(0,5)] public float FirstPipeDelay { get; private set; } = 1f;
    [field: SerializeField] public float PipeDestroyX { get; private set; } = -12f;
    [field: SerializeField, Range(0f, 10f)] public float PipeSpawnYRange { get; private set; } = 3f; 
    [field: SerializeField, Range(0f, 10f)] public float PipeSlideInTime { get; private set; } =.5f; 


    [field: Header("Bird")]
    [field: SerializeField] public float FlapForce { get; private set; } = 5f;
    [field: SerializeField] public float MaxFallSpeed { get; private set; } = -10f;
    [field: SerializeField] public float BirdMinAngle { get; private set; } = -90f;
    [field: SerializeField] public float BirdMaxAngle { get; private set; } = 30f;

    [field: Header("Pipe Slide In")]
    [field: SerializeField] public bool PipeSlideInEnabled { get; private set; } = true;

    [field: Header("Camera Shake")]
    [field: SerializeField] public float CameraShakeDuration { get; private set; } = 0.3f;
    [field: SerializeField] public float CameraShakeMagnitude { get; private set; } = 0.2f;
}
