using UnityEngine;

public enum SkillType
{
    Active,
    Passive
}

[CreateAssetMenu(fileName = "NewSkill", menuName ="Hero/Skill")]
public class SkillConfig : ScriptableObject
{
    [SerializeField] private string skillName;
    [SerializeField] private SkillType type;
    [SerializeField] private Sprite skillIcon;

    [SerializeField] private int baseCoolDown;
    [SerializeField] private int baseManaCost;
    [SerializeField] private int baseDamage;

    // Public read access
    public string SkillName
    {
        get => skillName;
    }

    public SkillType Type
    {
        get => type;
    }
    
    public Sprite SkillIcon
    {
        get => skillIcon;
    }
    
    public int BaseCoolDown
    {
        get => baseCoolDown;
    }
   
    public int BaseManaCost
    {
        get => baseManaCost;
    }

    public int BaseDamage
    {
        get => baseDamage;
    }
}
