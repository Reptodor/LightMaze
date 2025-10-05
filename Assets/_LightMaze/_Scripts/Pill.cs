using UnityEngine;

public class Pill : MonoBehaviour
{
    [SerializeField] private ShakeAnimationConfig _shakeAnimationConfig;
    [SerializeField] private int _healAmount;

    private ShakeAnimationHandler _shakeAnimationHandler;

    private void Awake()
    {
        _shakeAnimationHandler = new ShakeAnimationHandler(_shakeAnimationConfig, transform);   
    }

    private void OnEnable()
    {
        _shakeAnimationHandler.Start();
    }

    private void OnDisable()
    {
        _shakeAnimationHandler.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out Player player))
        {
            player.Heal(_healAmount);
            Destroy(gameObject);
        }
    }
}
