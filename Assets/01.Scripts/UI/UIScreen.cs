using UnityEngine;

public class UIScreen : UIBase
{
    public virtual void Awake() { }

    public virtual void Init() { }

    public override void OnShow()
    {
        gameObject.SetActive(true);
    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
    }
}
