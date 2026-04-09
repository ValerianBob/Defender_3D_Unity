using UnityEngine;
using UnityEngine.Rendering;

public class HeroInventory : MonoBehaviour
{
    private HeroController _heroController;

    private int CurrentGold;

    private const int MaxItemInInventory = 6;

    [SerializeField] private ItemConfig[] Items = new ItemConfig[MaxItemInInventory];

    public void Init(HeroController heroController)
    {
        _heroController = heroController;

        CurrentGold = 0;

        if (Items == null || Items.Length != MaxItemInInventory)
        {
            Items = new ItemConfig[MaxItemInInventory];
        }
    }

    public bool AddItemInInventory(ItemConfig item)
    {
        int count = GetItemsCount();

        if (count >= MaxItemInInventory)
        {
            Debug.Log("Inventory full");
            return false;
        }

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = item;

                Debug.Log($"Item :{item.name} added");
                return true;
            }
        }

        return false;
    }

    public bool SpawItemInInventory(int SwapFromItemId_1, int SwapToItemId_2)
    {
        if (SwapFromItemId_1 < 0 || SwapFromItemId_1 >= Items.Length &&
            SwapToItemId_2 < 0 || SwapToItemId_2 >= Items.Length)
        {
            Debug.Log("Idexes are incorrect");
            return false;
        }

        ItemConfig tempItem = Items[SwapFromItemId_1];
        Items[SwapFromItemId_1] = Items[SwapToItemId_2];
        Items[SwapToItemId_2] = tempItem;

        return true;
    }

    public bool RemoveItemFromInventory(int ItemIdForRemove)
    {
        int count = GetItemsCount();

        if (count == 0)
        {
            Debug.Log("Inventory is empty");
            return false;
        }
        else if (ItemIdForRemove < 0 || ItemIdForRemove >= Items.Length)
        {
            Debug.Log("Index is incorrect");
            return false;
        }

        for (int i = 0; i < Items.Length; i++)
        {
            if (i == ItemIdForRemove)
            {
                Items[i] = null;
                return true;
            }
        }

        return false;
    }

    public ItemConfig[] GetItems()
    {
        return Items;
    }

    public GameObject GetItemPrefabById(int ItemId)
    {
        return Items[ItemId].GetItemPrefab();
    }

    public int GetItemsCount()
    {
        int count = 0;

        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
            {
                count++;
            }
        }

        return count;
    }

    public int GetMaxItemsInInventory()
    {
        return MaxItemInInventory;
    }

    public int GetGold()
    {
        return CurrentGold;
    }

    public void SetGold(bool isAdding, int gold)
    {
        if (isAdding)
        {
            CurrentGold += gold;
        }
        else
        {
            CurrentGold -= gold;
        }
    }
}
