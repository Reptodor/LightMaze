using UnityEngine;

public class BaseHandTorch : MonoBehaviour
{
    private HandTorchConfig _handTorchConfig;
    private Player _player;
    private float _angle = 0;
    private bool _isInitialized = false;

    public void Initialize(HandTorchConfig handTorchConfig, Player player)
    {
        _handTorchConfig = handTorchConfig;
        _player = player;

        _isInitialized = true;
    }

    private void Update()
    {
        if(!_isInitialized)
            return;

        _angle += Time.deltaTime;

        var x = Mathf.Cos (_angle * _handTorchConfig.Speed) * _handTorchConfig.Radius;
        var y = Mathf.Sin (_angle * _handTorchConfig.Speed) * _handTorchConfig.Radius;
        transform.position = new Vector2(x, y) + (Vector2)_player.transform.position;
    }
}
