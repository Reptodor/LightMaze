using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraFollowingConfig", menuName = "Configs/Camera/Following")]
public class CameraFollowingConfig : ScriptableObject
{
    [SerializeField] private Vector3 _freePosition;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _followingOrthoSize;
    [SerializeField] private float _unfollowingOrthoSize;

    public Vector3 FreePosition => _freePosition;
    public float AnimationDuration => _animationDuration;
    public float FollowingOrthoSize => _followingOrthoSize;
    public float UnfollowingOrthoSize => _unfollowingOrthoSize;

    private void OnValidate()
    {
        if(_animationDuration < 0)
            throw new ArgumentOutOfRangeException(nameof(_animationDuration), "Animation duration cannot be below zero");

        if(_followingOrthoSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(_followingOrthoSize), "Following ortho size must be greater than zero");

        if(_unfollowingOrthoSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(_unfollowingOrthoSize), "Unfollowing ortho size must be greater than zero");
    }
}
