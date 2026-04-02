using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName ="Items/NewItem")]
public class ItemConfig : ScriptableObject
{
    [SerializeField] private string itemName;

    [SerializeField] private string itemDescription;

    [SerializeField] private Sprite itemIcon;

    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private float AttackDamage;
    [SerializeField] private float MagicDamage;

    [SerializeField] private float AttackRange;

    [SerializeField] private float MoveSpeed;
    [SerializeField] private float AttackSpeed;

    [SerializeField] private float Health;
    [SerializeField] private float Mana;

    [SerializeField] private float HealthGain;
    [SerializeField] private float ManaGain;
}
