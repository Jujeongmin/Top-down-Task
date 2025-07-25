using System.Collections.Generic;
using UnityEngine;

public class MonsterCodexUI : UIScreen
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private MonsterInfoPanel infoPanel;

    private List<MonsterSlot> slotList = new();

    public override void Awake()
    {
        base.Awake();
        Refresh();
    }

    public override void OnShow()
    {
        base.OnShow();
    }

    private void Refresh()
    {
        ClearSlots();

        var monsterLoader = DataManager.Instance.GetLoader<MonsterInfo, string>();
        if (monsterLoader == null) return;

        foreach (var monster in monsterLoader.ItemsList)
        {
            GameObject go = Instantiate(slotPrefab, slotParent);
            var slot = go.GetComponent<MonsterSlot>();
            slot.SetData(monster, () =>
            {
                infoPanel.ShowInfo(monster);
            });
            slotList.Add(slot);
        }
    }

    private void ClearSlots()
    {
        foreach (var slot in slotList)
        {
            Destroy(slot.gameObject);
        }
        slotList.Clear();
    }
}
