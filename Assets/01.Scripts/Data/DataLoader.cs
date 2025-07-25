using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDataLoader
{
}

public class DataLoader<T, TKey> : IDataLoader where T : IKeyedItem<TKey>
{
    public List<T> ItemsList { get; private set; }
    public Dictionary<TKey, T> ItemsDict { get; private set; }

    public List<T> DataList => ItemsList;

    public DataLoader(string path, string jsonRootKey)
    {
        string jsonData = Resources.Load<TextAsset>(path).text;

        switch (jsonRootKey)
        {
            case "Monster":
                var monsterWrapper = JsonUtility.FromJson<MonsterWrapper>(jsonData);
                ItemsList = monsterWrapper.Monster as List<T>;
                break;
            case "Item":
                var itemWrapper = JsonUtility.FromJson<ItemWrapper>(jsonData);
                ItemsList = itemWrapper.Item as List<T>;
                break;
            case "Quest":
                var questWrapper = JsonUtility.FromJson<QuestWrapper>(jsonData);
                ItemsList = questWrapper.Quest as List<T>;
                break;
            default:
                Debug.LogError($"Unsupported jsonRootKey: {jsonRootKey}");
                ItemsList = new List<T>();
                break;
        }

        if (ItemsList == null)
            ItemsList = new List<T>();

        ItemsDict = new Dictionary<TKey, T>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.Key, item);
        }
    }

    [Serializable]
    private class MonsterWrapper
    {
        public List<MonsterInfo> Monster;
    }

    [Serializable]
    private class ItemWrapper
    {
        public List<ItemInfo> Item;
    }

    [Serializable]
    private class QuestWrapper
    {
        public List<QuestInfo> Quest;
    }

    public T GetByKey(TKey key) => ItemsDict.TryGetValue(key, out var item) ? item : default;
    public T GetByIndex(int index) => (index >= 0 && index < ItemsList.Count) ? ItemsList[index] : default;
}

public interface IKeyedItem<T>
{
    T Key{ get; }
}