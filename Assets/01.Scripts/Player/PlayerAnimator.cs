using UnityEngine;

public class PlayerAnimator
{
    private readonly Animator animator;

    public PlayerAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public void SetIsRun(bool isRun) => animator.SetBool("IsRun", isRun);
    public void SetIsDead(bool isDead) => animator.SetBool("IsDead", isDead);
}
