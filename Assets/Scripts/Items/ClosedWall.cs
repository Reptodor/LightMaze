using System;
using DG.Tweening;
using UnityEngine;

public class ClosedWall : MonoBehaviour
{
    [SerializeField] private Tutorial _tutorial;
    
    private const float _tutorialAnimationStartSize = 0;
    private const float _tutorialAnimationEndSize = 7;
    private const float _tutorialAnimationDuration = 1;

    private void OnValidate()
    {
        if (_tutorial == null)
            throw new ArgumentNullException(nameof(_tutorial), "Tutorial cannot be null");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            _tutorial.gameObject.SetActive(true);
            _tutorial.transform.DOScale(_tutorialAnimationEndSize, _tutorialAnimationDuration)
                               .From(_tutorialAnimationStartSize).SetEase(Ease.OutBounce);
        }
    }
}
