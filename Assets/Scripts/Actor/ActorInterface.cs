public interface IDamageable
{
    Team DamageableTeam { get;}
    bool TakeDamage(Team team,int amount);
}

public interface ICanAttack
{

    void Attack();
}

