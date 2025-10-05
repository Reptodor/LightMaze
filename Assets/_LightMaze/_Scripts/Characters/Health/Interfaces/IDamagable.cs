using System;

public interface IDamagable
{
    public event Action<int> Damaged;

    void TakeDamage(int damage);
}
