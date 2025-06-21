using System;
using DG.Tweening;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private ClosedWall _closedWall;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _usedSprite;
    [SerializeField] private Tutorial _tutorial;

    private const float _tutorialAnimationStartSize = 0;
    private const float _tutorialAnimationEndSize = 7;
    private const float _tutorialAnimationDuration = 1;

    private void OnValidate()
    {
        if (_closedWall == null)
            throw new ArgumentNullException(nameof(_closedWall), "ClosedWall cannot be null");

        if (_spriteRenderer == null)
            throw new ArgumentNullException(nameof(_spriteRenderer), "SpriteRenderer cannot be null");

        if (_usedSprite == null)
            throw new ArgumentNullException(nameof(_usedSprite), "UsedSprite cannot be null");

        if (_tutorial == null)
            throw new ArgumentNullException(nameof(_tutorial), "Tutorial cannot be null");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        
        if (player != null)
        {
            _closedWall.gameObject.SetActive(false);
            _spriteRenderer.sprite = _usedSprite;
            _tutorial.gameObject.SetActive(true);
            _tutorial.transform.DOScale(_tutorialAnimationEndSize, _tutorialAnimationDuration)
                               .From(_tutorialAnimationStartSize).SetEase(Ease.OutBounce);
        }
    }
}
