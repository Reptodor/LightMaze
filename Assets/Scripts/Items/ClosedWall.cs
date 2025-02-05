using DG.Tweening;
using UnityEngine;

public class ClosedWall : MonoBehaviour
{
    [SerializeField] private Tutorial _tutorial;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(player != null)
        {
            _tutorial.gameObject.SetActive(true);
            _tutorial.transform.DOScale(7, 1).From(0).SetEase(Ease.OutBounce);
        }
    }
}
