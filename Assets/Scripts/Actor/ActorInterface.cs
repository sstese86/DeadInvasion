public interface IDamageable
{
    bool TakeDamage(Team team,int amount);
}

public interface ICanAttack
{

    void Attack();
}

public interface ICanMove
{
    void Movement();
    void Jump();
}
