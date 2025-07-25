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
        string dropItemIds = monster.Data.DropItem;

        if (string.IsNullOrEmpty(dropItemIds)) return;

        string[] itemIdStrings = dropItemIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var itemIdStr in itemIdStrings)
        {
            string trimmedId = itemIdStr.Trim();

            if (!int.TryParse(trimmedId, out int itemId))
            {
                Debug.LogWarning($"아이템 ID '{trimmedId}'를 int로 변환할 수 없음");
                continue;
            }

            var itemInfo = DataManager.Instance.GetItemInfo(itemId);
            if (itemInfo == null)
            {
                Debug.LogWarning($"아이템 ID {itemId}에 해당하는 정보 없음");
                continue;
            }

            GameObject dropPrefab = Resources.Load<GameObject>($"Drop/{itemInfo.Name}");
            if (dropPrefab != null)
            {
                GameObject.Instantiate(dropPrefab, monster.transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError($"프리팹 경로 없음: Drop/{itemInfo.Name}");
            }
        }
    }

    public void Update() { }

    public void Exit() { }
}
