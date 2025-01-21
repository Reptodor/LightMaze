using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Key : MonoBehaviour
{
    [SerializeField] private Light2D _light2D;
    private Sequence _animation;

    public Light2D Light2D => _light2D;

    private void OnValidate()
    {
        if(_light2D == null)
            _light2D = GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        StartShakeAnimation();
    }

    private void OnDisable()
    {
        StopShakeAnimation();
    }

    private void StartShakeAnimation()
    {
        _animation = DOTween.Sequence();

        _animation.Append(transform.DOPunchPosition(Vector2.right * 0.10f, 0.4f)).AppendInterval(3f).SetLoops(-1, LoopType.Restart);
    }

    private void StopShakeAnimation()
    {
        _animation.Kill();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null)
        {
            player.BagHandler.AddKey(1);

            Destroy(gameObject);
        }
    }
}
