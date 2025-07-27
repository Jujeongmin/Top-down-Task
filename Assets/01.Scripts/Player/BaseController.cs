using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 5f;
    protected Vector2 _movementDir;
    public Vector2 movementDir => _movementDir;

    protected Vector2 _lookDir;
    public Vector2 lookDir => _lookDir;

    protected Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HandleAction();
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected abstract void HandleAction();

    private void Move()
    {
        _rigidbody.velocity = _movementDir * _moveSpeed;
    }
}
