using System;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    public event Action OnClick;

    public void SetData(MonsterInfo info)
    {
        iconImage.sprite = info.Icon;
        GetComponent<Button>().onClick.AddListener(() => OnClick?.Invoke());
    }
}
