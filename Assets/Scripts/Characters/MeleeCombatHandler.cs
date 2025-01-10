using DG.Tweening;
using UnityEngine;

public class MeleeCombatHandler
{
    private MeleeCombatConfig _meleeCombatConfig;
    private bool _canAttack = true;
    private bool _isAttacking = false;

    public bool IsAttacking => _isAttacking;

    public MeleeCombatHandler(MeleeCombatConfig meleeCombatConfig)
    {
        _meleeCombatConfig = meleeCombatConfig;
    }


    public void Attack()
    {
        if(!_canAttack)
            return;
        
        _canAttack = false;
        _isAttacking = true;
        
        Sequence animation = DOTween.Sequence();
        animation.AppendInterval(_meleeCombatConfig.AnimationClip.length + 0.2f).AppendCallback(() => _isAttacking = false).AppendInterval(_meleeCombatConfig.AttackDelay).AppendCallback(() => AllowAttack());
    }

    private void AllowAttack()
    {
        _canAttack = true;
    }
}
