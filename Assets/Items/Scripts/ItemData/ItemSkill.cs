using UnityEngine;

public enum ItemExecuteType
{
    None,
    OnAttack,
    OnWalking,
    Active
}
public abstract class ItemSkill : ScriptableObject
{
    [SerializeField] private ItemExecuteType type;

    public ItemExecuteType GetItemSkillType()
    {
        return type;
    }

    public virtual void Execute(HeroEventsArgs HeroArgs)
    { 
    }
}
