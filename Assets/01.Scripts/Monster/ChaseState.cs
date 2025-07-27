public interface IMonsterState
{
    void Enter(Monster monster);
    void Update();
    void Exit();
}

public class ChaseState : IMonsterState
{
    Monster monster;

    public void Enter(Monster monster)
    {
        this.monster = monster;
    }

    public void Update()
    {
        if (monster.ShouldDie()) return;

        if (monster.IsInAttackRange())
        {
            monster.ChangeState(new AttackState());
            return;
        }

        monster.MoveTowardsTarget();
    }

    public void Exit() { }
}