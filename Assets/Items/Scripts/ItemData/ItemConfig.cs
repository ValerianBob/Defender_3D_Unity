using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Items/NewItem")]
public class ItemConfig : ScriptableObject, IShowInfo
{
    [SerializeField] private string itemName;

    [SerializeField] private string itemDescription;

    [SerializeField] private Sprite itemIcon;

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private int AttackDamage;
    [SerializeField] private int MagicDamage;

    [SerializeField] private int AttackRange;

    [SerializeField] private int MoveSpeed;
    [SerializeField] private float AttackSpeed;

    [SerializeField] private int Health;
    [SerializeField] private int Mana;

    [SerializeField] private int HealthGain;
    [SerializeField] private int ManaGain;

    [SerializeField] private int ItemCost;

    public string GetItemName()
    {
        return itemName;
    }

    public Sprite GetItemIcon()
    {
        return itemIcon;
    }

    public GameObject GetItemPrefab()
    {
        return itemPrefab;
    }

    public int GetItemDamage()
    {
        return AttackDamage;
    }

    public int GetItemMagicDamage()
    {
        return MagicDamage;
    }

    public int GetItemAttackRange()
    {
        return AttackRange;
    }

    public int GetItemMoveSpeed()
    {
        return MoveSpeed;
    }

    public float GetItemAttackSpeed()
    {
        return AttackSpeed;
    }

    public int GetItemHealth()
    {
        return Health;
    }

    public int GetItemMana()
    {
        return Mana;
    }

    public int GetItemHealthGain()
    {
        return HealthGain;
    }

    public int GetItemManaGain()
    {
        return ManaGain;
    }

    public int GetItemCost()
    {
        return ItemCost;
    }

    public string GetTitle()
    {
        return itemName;
    }

    public string GetDescription()
    {
        return $"{itemDescription}\n" +
            $"Attack Damage: +{AttackDamage}\n" +
       $"Magic Damage: +{MagicDamage}\n" +
       $"Attack Range: +{AttackRange}\n" +
       $"Move Speed: +{MoveSpeed}\n" +
       $"Attack Speed: -{AttackSpeed}";
    }
}
