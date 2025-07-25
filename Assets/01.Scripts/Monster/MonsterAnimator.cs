using UnityEngine;

public class MonsterAnimator
{
    private readonly Animator animator;

    public MonsterAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void SetIsRun(bool isRun) => animator.SetBool("IsRun", isRun);
    public void SetIsDead(bool isDead) => animator.SetBool("IsDead", isDead);
    public void TriggerHit() => animator.SetTrigger("IsHit");
}
