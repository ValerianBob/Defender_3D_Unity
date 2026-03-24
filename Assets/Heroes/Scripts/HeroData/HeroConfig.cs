using UnityEngine;

[CreateAssetMenu(fileName = "HeroConfig", menuName = "Game/Hero Config")]
public class HeroConfig : ScriptableObject
{
    public float AttackDamage;
    public float MagicDamage;

    public float AttackRange;

    public float MoveSpeed;
    public float AttackSpeed;

    public float MaxHealth;
    public float MaxMana;
}
