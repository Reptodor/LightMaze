using System.Collections;
using UnityEngine;

namespace LightMaze._Scripts.Characters.AttackSystems
{
    public class TriggerAttackSystem : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _cooldownTime;

        private bool _canAttack = true;

        private void OnEnable()
        {
            _canAttack = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_canAttack)
                return;

            if (other.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_damage);
                StartCoroutine(StartCooldown());
            }
        }

        private IEnumerator StartCooldown()
        {
            _canAttack = false;

            yield return new WaitForSeconds(_cooldownTime);

            _canAttack = true;
        }
    }
}