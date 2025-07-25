using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;

    protected Vector2 _movementDir = Vector2.zero;
    public Vector2 movementDir { get { return _movementDir; } }

    protected Vector2 _lookDir = Vector2.zero;
    public Vector2 lookDir { get { return _lookDir; } }

    private PlayerAnimator playerAnimator;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = new PlayerAnimator(GetComponent<Animator>());
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(_lookDir);
    }

    protected virtual void FixedUpdate()
    {
        Movement(_movementDir);

        bool isRunning = _movementDir.sqrMagnitude > 0.01f;
        playerAnimator.SetIsRun(isRunning);
    }

    protected virtual void HandleAction() { }

    private void Movement(Vector2 dir)
    {
        dir = dir * 5;       
        _rigidbody.velocity = dir;
    }

    private void Rotate(Vector2 dir)
    {
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;
    }
}
