using UnityEngine;
using UnityEngine.UI;

public class MonsterHpBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void Init(int maxHP)
    {
        slider.maxValue = maxHP;
        slider.value = maxHP;
    }

    public void UpdateHP(int currentHP)
    {
        slider.value = currentHP;
    }
}