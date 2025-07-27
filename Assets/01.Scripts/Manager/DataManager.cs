using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonBehaviour<DataManager>
{
    private Dictionary<Type, IDataLoader> _dataLoaderMap = new();

    protected override void Awake()
    {
        base.Awake();
        RegisterLoaders();
    }

    private void RegisterLoaders()
    {
        RegisterLoader<MonsterInfo, string>("JSON/MonsterData", "Monster");
        RegisterLoader<ItemInfo, int>("JSON/ItemData", "Item");
        RegisterLoader<QuestInfo, string>("JSON/QuestData", "Quest");
    }

    private void RegisterLoader<T, Tkey>(string path, string rootkey) where T : IKeyedItem<Tkey>
    {
        _dataLoaderMap[typeof(T)] = new DataLoader<T, Tkey>(path, rootkey);
    }

    public DataLoader<T, TKey> GetLoader<T, TKey>() where T : IKeyedItem<TKey>
    {
        if (_dataLoaderMap.TryGetValue(typeof(T), out var loader))
            return loader as DataLoader<T, TKey>;

        Debug.LogError($"{typeof(T).Name} 타입의 데이터 로더가 없음");
        return null;
    }

    public ItemInfo GetItemInfo(int itemID)
    {
        var loader = GetLoader<ItemInfo, int>();
        return loader?.GetByKey(itemID);
    }    
}
