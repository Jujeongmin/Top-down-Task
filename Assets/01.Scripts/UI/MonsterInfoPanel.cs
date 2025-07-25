using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInfoPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI HpText;
    [SerializeField] private TextMeshProUGUI AttackRangeText;
    [SerializeField] private TextMeshProUGUI MoveSpeedText;

    public void ShowInfo(MonsterInfo info)
    {
        icon.sprite = info.Icon;
        nameText.text = info.Name;
        descriptionText.text = $"Description :\n{info.Description}";
        attackText.text = $"Attack : {Mathf.RoundToInt(info.Attack * (1f + info.AttackMul))}";
        HpText.text = $"Hp : {Mathf.RoundToInt(info.MaxHP * (1f + info.MaxHPMul))}";
        AttackRangeText.text = $"AttackRange :\n{info.AttackRange * (1f + info.AttackRangeMul)}";
        MoveSpeedText.text = $"MoveSpeed :\n{info.MoveSpeed}";
    }
}
