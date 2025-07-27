using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected Transform _playerTransform;

    public void SetPlayer(Transform player)
    {
        _playerTransform = player;
    }

    protected virtual void Start() { }

    protected virtual void Update()
    {
        HandleWeapon();
    }

    protected abstract void HandleWeapon();
}

public interface IAttackable
{
    void TakeDamage(int damage);
}
