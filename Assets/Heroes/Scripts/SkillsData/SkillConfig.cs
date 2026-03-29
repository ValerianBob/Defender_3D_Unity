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

    [SerializeField] private float baseCoolDown;
    [SerializeField] private float baseManaCost;
    [SerializeField] private float baseDamage;

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
    
    public float BaseCoolDown
    {
        get => baseCoolDown;
    }
   
    public float BaseManaCost
    {
        get => baseManaCost;
    }

    public float BaseDamage
    {
        get => baseDamage;
    }
}
