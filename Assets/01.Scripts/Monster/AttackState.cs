using UnityEngine;

public class AttackState : IMonsterState
{
    private Monster monster;
    private float lastAttackTime;

    public void Enter(Monster monster)
    {
        this.monster = monster;
        lastAttackTime = Time.time - (1f / monster.Data.AttackSpeed);
    }

    public void Update()
    {
        if (monster.ShouldDie()) return;

        if (!monster.IsInAttackRange())
        {
            monster.ChangeState(new ChaseState());
            return;
        }

        if (Time.time - lastAttackTime >= 1f / monster.Data.AttackSpeed)
        {
            monster.Attack();
            lastAttackTime = Time.time;
        }
    }

    public void Exit() { }
}
