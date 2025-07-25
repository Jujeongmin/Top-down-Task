using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterInfo Data { get; private set; }
    public string MonsterID { get; private set; }
    private IMonsterState currentState;

    private Transform target;
    private int currentHP;

    [SerializeField] private LayerMask playerLayer;

    private float searchInterval = 1f;
    private float searchTimer = 0f;

    private MonsterAnimator animator;

    [SerializeField] private MonsterHpBar hpBar;
    private int maxHP;

    private void Awake()
    {
        animator = new MonsterAnimator(GetComponent<Animator>());
        animator.SetIsDead(false);
    }

    public void Init(MonsterInfo data, string monsterID)
    {
        Data = data;
        MonsterID = monsterID;
        maxHP = Mathf.RoundToInt(Data.MaxHP * (1f + Data.MaxHPMul));
        currentHP = maxHP;

        hpBar.Init(maxHP);
        hpBar.transform.localPosition = new Vector3(0, 0.8f, 0);

        FindTargetByLayer();
        ChangeState(new ChaseState());
    }

    void Update()
    {
        searchTimer += Time.deltaTime;
        if (searchTimer >= searchInterval)
        {
            searchTimer = 0f;
            FindTargetByLayer();
        }

        currentState?.Update();
    }

    private void FindTargetByLayer()
    {
        if (target != null) return;

        Collider2D[] candidates = Physics2D.OverlapCircleAll(transform.position, 50f, playerLayer);

        float minDist = float.MaxValue;
        Transform closest = null;

        foreach (var col in candidates)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = col.transform;
            }
        }

        if (closest != null)
            target = closest;
    }

    public void ChangeState(IMonsterState newState)
    {
        if (currentState == newState) return;
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public void MoveTowardsTarget()
    {
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * Data.MoveSpeed * Time.deltaTime;
        transform.localScale = new Vector3(dir.x >= 0 ? 1 : -1, 1, 1);
        animator.SetIsRun(true);
    }

    public bool IsInAttackRange()
    {
        if (target == null) return false;

        float range = Data.AttackRange * (1f + Data.AttackRangeMul);
        return Vector3.Distance(transform.position, target.position) <= range;
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        animator.TriggerHit();

        hpBar.UpdateHP(currentHP);

        if (IsDead())
        {
            ChangeState(new DeadState());
        }
    }

    public void Attack()
    {
        int finalAttack = Mathf.RoundToInt(Data.Attack * (1f + Data.AttackMul));

        PlayerHp playerHp = target.GetComponent<PlayerHp>();
        if (playerHp != null)
        {
            playerHp.TakeDamage(finalAttack);
        }
    }

    public void Die()
    {
        animator.SetIsDead(true);
        StartCoroutine(DelayedDisable(1f));
    }

    private IEnumerator DelayedDisable(float delay)
    {
        yield return new WaitForSeconds(delay);
        MonsterSpawnManager.Instance.ReturnToPool(gameObject);
    }

    public void ResetMonster()
    {
        currentHP = maxHP;
        hpBar.UpdateHP(currentHP);
        animator.SetIsDead(false);
        ChangeState(new ChaseState());
        FindTargetByLayer();
    }
}
