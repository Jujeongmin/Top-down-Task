using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected virtual void Update()
    {
        HandleWeapon();
    }

    protected abstract void HandleWeapon(); // 무기 타입별 동작 (회전, 발사 등)
}
