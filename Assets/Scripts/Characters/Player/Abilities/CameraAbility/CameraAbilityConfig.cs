using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCameraAbilityConfig", menuName = "Configs/Abilities/Camera")]
public class CameraAbilityConfig : AbilityConfig
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _followingOrthoSize;

    public float AnimationDuration => _animationDuration;
    public float FollowingOrthoSize => _followingOrthoSize;

    protected override void OnValidate()
    {
        base.OnValidate();
        
        if (_animationDuration < 0)
            throw new ArgumentOutOfRangeException(nameof(_animationDuration), "Animation duration cannot be below zero");

        if(_followingOrthoSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(_followingOrthoSize), "Following ortho size must be greater than zero");
    }
}
