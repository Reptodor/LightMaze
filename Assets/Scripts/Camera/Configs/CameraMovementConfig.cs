using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraMovementConfig", menuName = "Configs/Camera/Movement")]
public class CameraMovementConfig : ScriptableObject
{
    [SerializeField] private Vector3 _offset;
    [Range(1, 10)]
    [SerializeField] private float _smoothFactor;

    public Vector3 Offset => _offset;
    public float SmoothFactor => _smoothFactor;
}
