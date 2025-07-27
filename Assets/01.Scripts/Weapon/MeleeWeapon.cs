using UnityEngine;

public class MeleeWeapon : WeaponBase
{    
    [SerializeField] private float rotationRadius = 1.5f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask enemyLayer;

    protected override void Start()
    {
        base.Start();
        if (_playerTransform == null && transform.parent != null)
        {
            SetPlayer(transform.parent);
        }
    }

    protected override void HandleWeapon()
    {        
        if (_playerTransform == null) return;

        float angle = rotationSpeed * Time.deltaTime;
        Vector3 dir = (transform.position - _playerTransform.position).normalized;
        dir = Quaternion.Euler(0, 0, angle) * dir;

        transform.position = _playerTransform.position + dir * rotationRadius;
        transform.right = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) == 0)
            return;

        if (collision.TryGetComponent(out IAttackable target))
        {
            target.TakeDamage(damage);
        }
    }
}
