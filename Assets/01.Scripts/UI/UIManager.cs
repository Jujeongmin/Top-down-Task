using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    public Dictionary<string, UIBase> UIList = new();

    public T Show<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;

        if (UIList.TryGetValue(uiName, out var existingUI))
        {
            existingUI.gameObject.SetActive(true);
            existingUI.OnShow();
            return existingUI as T;
        }

        var ui = Resources.Load<UIBase>($"UI/{uiName}") as T;

        T instantiated = Object.Instantiate(ui);
        instantiated.OnShow();
        UIList.Add(uiName, instantiated);
        return instantiated;
    }

    public void Hide<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;

        if (UIList.TryGetValue(uiName, out var ui))
        {
            ui.OnHide();
            ui.gameObject.SetActive(false);
        }
    }

    public T Get<T>() where T : UIBase
    {
        string uiName = typeof(T).Name;

        if (UIList.TryGetValue(uiName, out var ui))
        {
            return ui as T;
        }

        return null;
    }
}
