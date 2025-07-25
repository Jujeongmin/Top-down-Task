using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    public int maxHP = 10000;
    private int currentHP;

    private PlayerAnimator playerAnimator;

    private void Awake()
    {
        currentHP = maxHP;
        playerAnimator = new PlayerAnimator(GetComponent<Animator>());
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        playerAnimator.SetIsDead(true);
    }
}
