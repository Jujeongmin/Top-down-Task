using System;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    private Action onClick;

    public void SetData(MonsterInfo info, Action onClickCallback)
    {
        iconImage.sprite = info.Icon;
        onClick = onClickCallback;
        GetComponent<Button>().onClick.AddListener(() => onClick?.Invoke());
    }
}
