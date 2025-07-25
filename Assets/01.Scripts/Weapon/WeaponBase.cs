using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected virtual void Update()
    {
        HandleWeapon();
    }

    protected abstract void HandleWeapon(); // ���� Ÿ�Ժ� ���� (ȸ��, �߻� ��)
}
