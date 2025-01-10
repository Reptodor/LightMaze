using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeCombatConfig", menuName = "Configs/Characters/Combat/MeleeCombat")]
public class MeleeCombatConfig : ScriptableObject
{
    [SerializeField] private AnimationClip _animationClip;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;

    public AnimationClip AnimationClip => _animationClip;
    public int Damage => _damage;
    public float AttackDelay => _attackDelay;

    private void OnValidate()
    {
        if(_animationClip == null)
            throw new ArgumentNullException(nameof(_animationClip), "Animation clip cannot be null");

        if(_damage <= 0)
            throw new ArgumentOutOfRangeException(nameof(_damage), "Damage must be greater than null");

        if(_attackDelay <= 0)
            throw new ArgumentOutOfRangeException(nameof(_attackDelay), "Attack delay must be greater than null");
    }
}
