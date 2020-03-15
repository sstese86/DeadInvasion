public interface IDamageable
{
    void TakeDamage(Team team,int amount);
}

public interface ICanAttack
{

    void Attack();
}

public interface ICanMove
{
    void Move();
    void Jump();
}
