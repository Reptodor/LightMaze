using UnityEngine;

public class Mine : MonoBehaviour
{
    private Explosion _explosion;

    public void Initialize(Explosion explosion)
    {
        _explosion = explosion;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable DamagableObject))
        {
            _explosion.transform.position = transform.position;
            _explosion.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
