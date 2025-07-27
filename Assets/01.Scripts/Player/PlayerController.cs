using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;

public class PlayerController : BaseController
{
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Animator animator;

    private PlayerAnimator playerAnimator;

    protected override void Awake()
    {
        base.Awake();
        playerAnimator = new PlayerAnimator(animator);
    }

    protected override void Update()
    {
        base.Update();
        Rotate(_lookDir);
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        bool isRunning = _movementDir.sqrMagnitude > 0.01f;
        playerAnimator.SetIsRun(isRunning);
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _movementDir = new Vector2(horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        _lookDir = (worldPosition - (Vector2)transform.position);

        _lookDir = _lookDir.magnitude < 0.9f ? Vector2.zero : _lookDir.normalized;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            var existing = UIManager.Instance.Get<MonsterCodexUI>();

            if (existing != null && existing.gameObject.activeSelf)
            {
                UIManager.Instance.Hide<MonsterCodexUI>();
                Time.timeScale = 1f;
            }
            else
            {
                UIManager.Instance.Show<MonsterCodexUI>();
                Time.timeScale = 0f;
            }
        }
    }

    private void Rotate(Vector2 dir)
    {
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
    }
}
