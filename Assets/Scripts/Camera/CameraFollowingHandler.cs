using UnityEngine;
using DG.Tweening;
using System;

public class CameraFollowingHandler
{
    private Camera _camera;
    private Player _player;
    private CameraFollowingConfig _config;
    private CameraMovementHandler _cameraMovementHandler;
    private bool _isFollowing = false;

    public bool IsFollowing => _isFollowing;

    public CameraFollowingHandler(Camera camera, Player player, CameraFollowingConfig cameraFollowingConfig, CameraMovementHandler cameraMovementHandler)
    {
        _camera = camera;
        _player = player;
        _config = cameraFollowingConfig;
        _cameraMovementHandler = cameraMovementHandler;
    }

    public void Switch()
    {
        if(_isFollowing)
        {
            UnFollow();
        }
        else
        {
            Follow();
        }
    }

    private void Follow()
    {
        Sequence animation = DOTween.Sequence();

        animation.Append(_camera.transform.DOMove(GetEndPosition(), _config.AnimationDuration))
                  .Join(_camera.DOOrthoSize(_config.FollowingOrthoSize, _config.AnimationDuration))
                  .AppendCallback(() => _isFollowing = true)
                  .AppendCallback(() => _cameraMovementHandler.SetFollowing(_isFollowing));
        
    }

    private Vector3 GetEndPosition()
    {
        Vector3 endPosition = new Vector3(_player.transform.position.x, _player.transform.position.y, _config.FreePosition.z);

        return endPosition;
    }

    private void UnFollow()
    {
        Sequence animation = DOTween.Sequence();

        animation.Append(_camera.transform.DOMove(_config.FreePosition, _config.AnimationDuration))
                  .Join(_camera.DOOrthoSize(_config.UnfollowingOrthoSize, _config.AnimationDuration))
                  .AppendCallback(() => _isFollowing = false)
                  .AppendCallback(() => _cameraMovementHandler.SetFollowing(_isFollowing));
    }
}
