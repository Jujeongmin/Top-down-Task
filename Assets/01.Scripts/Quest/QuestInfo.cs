using System;

[Serializable]
public class QuestInfo : IKeyedItem<string>
{
    public string QuestID;
    public int Type;
    public int NPC;
    public string Name;
    public int Goal;
    public string Description;
    public int PriorID;
    public int GoalID;
    public int Exp;
    public int Gold;
    public bool Clear;
    public int Reward;

    public string Key => QuestID;
}