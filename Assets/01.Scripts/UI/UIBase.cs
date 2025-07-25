using UnityEngine;

public class UIBase : MonoBehaviour
{
    public virtual void OnShow() => gameObject.SetActive(true);
    public virtual void OnHide() => gameObject.SetActive(false);
}
