using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class HeroInventory : MonoBehaviour
{
    private HeroController _heroController;

    public int CurrentGold;

    private const int MaxItemInInventory = 6;

    [SerializeField] private List<ItemConfig> items = new List<ItemConfig>();

    public void Init(HeroController heroController)
    {
        _heroController = heroController;

        CurrentGold = 0;
    }

    public bool AddItemInInventory(ItemConfig item)
    {
        if (items.Count >= MaxItemInInventory)
        {
            Debug.Log("Inventory full");
            return false;
        }

        items.Add(item);

        Debug.Log($"Item :{item.name} added");

        return true;
    }
}
