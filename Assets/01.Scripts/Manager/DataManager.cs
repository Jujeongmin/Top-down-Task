using System;
using System.Collections.Generic;

public class DataManager : SingletonBehaviour<DataManager>
{
    private Dictionary<Type, IDataLoader> _dataLoaderMap;

    protected override void Awake()
    {
        base.Awake();
        _dataLoaderMap = new()
        {
            { typeof(MonsterInfo), new DataLoader<MonsterInfo, string>("JSON/MonsterData", "Monster") },
            { typeof(ItemInfo), new DataLoader<ItemInfo, int>("JSON/ItemData", "Item") },
            { typeof(QuestInfo), new DataLoader<QuestInfo, string>("JSON/QuestData", "Quest") }
        };
    }

    public DataLoader<T, TKey> GetLoader<T, TKey>() where T : IKeyedItem<TKey>
    {
        if (_dataLoaderMap.TryGetValue(typeof(T), out var loader))
            return loader as DataLoader<T, TKey>;

        UnityEngine.Debug.LogError($"Loader for type {typeof(T)} not found.");
        return null;
    }

    public ItemInfo GetItemInfo(int itemID)
    {
        var loader = GetLoader<ItemInfo, int>();
        return loader?.GetByKey(itemID);
    }
}
