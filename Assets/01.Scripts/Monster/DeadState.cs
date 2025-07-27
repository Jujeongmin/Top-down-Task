using System;
using UnityEngine;

public class DeadState : IMonsterState
{
    private Monster monster;

    public void Enter(Monster monster)
    {
        this.monster = monster;
        DropItem();
        monster.Die();
    }

    private void DropItem()
    {
        if (string.IsNullOrWhiteSpace(monster.Data.DropItem)) return;

        string[] itemIdStrings = monster.Data.DropItem.Split(',');

        foreach (var itemIdStr in itemIdStrings)
        {
            if (int.TryParse(itemIdStr.Trim(), out int itemId))
            {                
                var info = DataManager.Instance.GetItemInfo(itemId);
                if(info == null) continue;

                var prefab = Resources.Load<GameObject>($"Drop/{info.Name}");
                if(prefab != null)
                {
                    GameObject.Instantiate(prefab, monster.transform.position, Quaternion.identity);   
                }
            }
        }
    }

    public void Update() { }

    public void Exit() { }
}
