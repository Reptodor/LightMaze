using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "Configs/Level")]
public class LevelConfig : ScriptableObject
{
    [Header("Camera")]
    [SerializeField] private Vector3 _cameraFreePosition;
    [SerializeField] private float _cameraUnfollowingOrthoSize;

    [Header("Keys")]
    [SerializeField] private int _keysCount;

    public Vector3 CameraFreePosition => _cameraFreePosition;
    public float CameraUnfollowingOrthoSize => _cameraUnfollowingOrthoSize;
    public int KeysCount => _keysCount;

    private void OnValidate()
    {
        if(_cameraUnfollowingOrthoSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(_cameraUnfollowingOrthoSize), "Camera unfollowing ortho size must be greater than zero");
    
        if (_keysCount < 0)
            throw new ArgumentOutOfRangeException(nameof(_keysCount), "Keys count cannot be below zero");
    }
}
