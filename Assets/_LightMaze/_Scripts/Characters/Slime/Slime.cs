using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _stopDistance = 0.5f;
    [SerializeField] private float _chaseSpeed = 3.5f;

    [Header("Vision")]
    [SerializeField] private float _visionRange = 5f;
    [SerializeField] private LayerMask _obstacleLayer; 

    private Player _player;
    private Rigidbody2D _rigidbody;

    private bool _isInitialized;

    public void Initialize(Player player)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _player = player;

        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isInitialized)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);

        if (!CanSeePlayer(distanceToPlayer) || distanceToPlayer <= _stopDistance)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        Chase();   
    }

    private bool CanSeePlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > _visionRange)
            return false;

        Vector2 directionToPlayer = ((Vector2)_player.transform.position - (Vector2)transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, _obstacleLayer);

        return hit.collider == null;
    }

    private void Chase()
    {
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        _rigidbody.velocity = direction * _chaseSpeed;

        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        if (_player.transform.position.x < transform.position.x)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);  
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _visionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stopDistance);
    }
#endif
}
