using UnityEngine;

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

    public void Start()
    {
        //Maybe Change Later
        HeroEvents.OnGoldGainHendler?.Invoke(CurrentGold);
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

    public ItemConfig[] GetItems()
    {
        return Items;
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
