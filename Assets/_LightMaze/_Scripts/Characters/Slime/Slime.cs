using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private enum State { Idle, Chase, Attack, Return }
    private State currentState;

    [Header("Movement")]
    public float moveSpeed = 2f;
    public float chaseSpeed = 3.5f;
    private Vector2 startPosition;
    private Player _player;
    private Rigidbody2D rb;

    [Header("Vision")]
    public float visionRange = 5f;
    public LayerMask obstacleLayer; // Слой для стен, которые блокируют обзор
    [SerializeField] private LayerMask _playerLayer;

    [Header("Attack")]
    public float attackRange = 0.5f;
    public int attackDamage = 1;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    private bool _isInitialized;

    public void Initialize(Player player)
    {
        rb = GetComponent<Rigidbody2D>();
        _player = player;
        startPosition = transform.position;
        currentState = State.Idle; // Начальное состояние

        _isInitialized = true;
    }

    void Update()
    {
        if (!_isInitialized)
            return;

        Debug.Log(CanSeePlayer());

        // Конечный автомат
        switch (currentState)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
            case State.Return:
                ReturnUpdate();
                break;
        }
    }

    void IdleUpdate()
    {
        // Проверяем, видим ли мы игрока?
        if (CanSeePlayer())
        {
            currentState = State.Chase;
            return;
        }

        // Здесь можно добавить патрулирование или просто стояние на месте
        // Например: rb.velocity = new Vector2(...);
    }

    void ChaseUpdate()
    {
        // Если игрок ушел из поля зрения, возвращаемся
        if (!CanSeePlayer())
        {
            currentState = State.Return;
            return;
        }

        // Если игрок в зоне атаки, атакуем
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        if (distanceToPlayer <= attackRange)
        {
            currentState = State.Attack;
            return;
        }

        // Двигаемся к игроку
        Vector2 direction = (_player.transform.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;
    }

    void AttackUpdate()
    {
        // Останавливаемся при атаке
        rb.velocity = Vector2.zero;

        // Проверяем, не убежал ли игрок
        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        if (distanceToPlayer > attackRange)
        {
            currentState = State.Chase; // Возвращаемся к преследованию
            return;
        }

        // Наносим урон с кд
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Здесь логика нанесения урона игроку
            // Например: player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            Debug.Log("Slime attacks for " + attackDamage + " damage!");
            lastAttackTime = Time.time;
        }
    }

    void ReturnUpdate()
    {
        // Двигаемся к стартовой точке
        Vector2 direction = (startPosition - (Vector2)transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        // Если вернулись "достаточно близко", переходим в покой
        if (Vector2.Distance(transform.position, startPosition) < 0.1f)
        {
            rb.velocity = Vector2.zero;
            currentState = State.Idle;
        }

        // Если по пути обратно снова увидели игрока, преследуем!
        if (CanSeePlayer())
        {
            currentState = State.Chase;
        }
    }

    // Главный метод проверки видимости игрока
    bool CanSeePlayer()
    {
        // 1. Проверка дистанции
        if (Vector2.Distance(transform.position, _player.transform.position) > visionRange)
            return false;

        // 2. Проверка на наличие препятствий (стен) с помощью Raycast
        Vector2 directionToPlayer = (_player.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, visionRange, obstacleLayer);
        RaycastHit2D hitInPlayer = Physics2D.Raycast(transform.position, directionToPlayer, visionRange, _playerLayer);
        Debug.Log(hit.collider);
        // Если луч НЕ уперся в стену (или уперся именно в игрока), значит, видим его
        if (hit.collider == null)
        {
            Debug.Log("Chase");
            return true;
        }

        return false;
    }

    // Визуализация в редакторе (очень полезно для отладки)
    void OnDrawGizmosSelected()
    {
        // Рисуем зону видимости
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        // Рисуем зону атаки
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Если в редакторе есть ссылка на игрока, рисуем луч
        if (_player != null && Application.isPlaying)
        {
            Gizmos.color = CanSeePlayer() ? Color.red : Color.green;
            Gizmos.DrawLine(transform.position, _player.transform.position);
        }
    }
}
