using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int _damage = 1;

    public void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable DamagableObject))
        {
            DamagableObject.TakeDamage(_damage);
        }
    }
}
