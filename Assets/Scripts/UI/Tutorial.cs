using DG.Tweening;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private Sequence _sequence;

    private void OnDestroy()
    {
        _sequence.Kill();
    }

    public void Close()
    {
        _sequence = DOTween.Sequence();
        _sequence.Append(transform.DOScale(0, 1).From(7).SetEase(Ease.InBack)).AppendCallback(() => gameObject.SetActive(false));
    }
}
