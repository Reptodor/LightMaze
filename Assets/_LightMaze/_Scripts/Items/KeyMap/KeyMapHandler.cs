using UnityEngine;

public class KeyMapHandler : MonoBehaviour
{
    [SerializeField] ShakeAnimationConfig _shakeAnimationConfig;
    
    private Key[] _keys;
    private ShakeAnimationHandler _shakeAnimationHandler;
    private bool _isInitialized = false;

    public void Initialize(Key[] keys)
    {
        _keys = keys;
        _shakeAnimationHandler = new ShakeAnimationHandler(_shakeAnimationConfig, transform);

        _isInitialized = true;
        OnEnable();
    }

    private void OnEnable()
    {
        if(_isInitialized)
            _shakeAnimationHandler.Start();
    }

    private void OnDisable()
    {
        _shakeAnimationHandler.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            foreach(Key key in _keys)
            {
                key?.EnableLight();
            }

            Destroy(gameObject);
        }
    }
}
