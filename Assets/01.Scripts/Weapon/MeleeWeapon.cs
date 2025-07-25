using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    public Transform playerPos;
    public float rotationRadius = 1.5f;
    public float rotationSpeed = 180f;
    
    public int damage = 10;
    public LayerMask enemyLayer;

    protected override void HandleWeapon()
    {        
        if (playerPos == null) return;

        float angle = rotationSpeed * Time.deltaTime;
        Vector3 dir = (transform.position - playerPos.position).normalized;
        dir = Quaternion.Euler(0, 0, angle) * dir;
        transform.position = playerPos.position + dir * rotationRadius;
        transform.right = dir;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) == 0)
            return;

        var monster = collision.GetComponent<Monster>();
        if (monster != null)
        {
            monster.TakeDamage(damage);
        }
    }
}
