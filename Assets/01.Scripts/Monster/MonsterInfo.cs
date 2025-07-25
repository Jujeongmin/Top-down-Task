using UnityEngine;

[System.Serializable]
public class MonsterInfo : IKeyedItem<string>
{    
    public string MonsterID;
    public string Name;
    public string Description;
    public int Attack;
    public float AttackMul;
    public int MaxHP;
    public float MaxHPMul;
    public int AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;
    public int MinExp;
    public int MaxExp;
    public string DropItem;

    public string Key => MonsterID;

    public Sprite Icon => Resources.Load<Sprite>($"Icons/{Name}");
}