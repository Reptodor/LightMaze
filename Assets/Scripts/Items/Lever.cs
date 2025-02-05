using DG.Tweening;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private ClosedWall _closedWall;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _usedSprite;
    [SerializeField] private Tutorial _tutorial;

    private void OnValidate()
    {
        if(_spriteRenderer == null)
            _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        
        if (player != null)
        {
            _closedWall.gameObject.SetActive(false);
            _spriteRenderer.sprite = _usedSprite;
            _tutorial.gameObject.SetActive(true);
            _tutorial.transform.DOScale(7, 1).From(0).SetEase(Ease.OutBounce);
        }
    }
}
