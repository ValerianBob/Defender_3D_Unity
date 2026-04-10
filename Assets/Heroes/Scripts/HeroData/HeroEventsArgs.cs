public class HeroEventsArgs
{
    public SkillConfig[] SkillsData { get; private set; }

    public HeroAttributes CurrentHeroAttributes { get; private set; }

    public HeroInventory CurrentHeroInventory { get; private set; }

    public HeroController HeroController { get; private set; }

    public int[] LevelsOfSkills { get; private set; }

    public int SkillId { get; private set; }

    public int SkillLevelId {  get; private set; }

    public ItemExecuteType ItemExecuteType { get; private set; }

    public HeroEventsArgs() 
    {
    }

    public HeroEventsArgs(SkillConfig[] skillsData)
    {
        SkillsData = skillsData;
    }

    public HeroEventsArgs(HeroAttributes currentHeroAttributes)
    {
        CurrentHeroAttributes = currentHeroAttributes;
    }

    public HeroEventsArgs(HeroInventory currentHeroInventory)
    {
        CurrentHeroInventory = currentHeroInventory;
    }

    public HeroEventsArgs(HeroController controller)
    {
        HeroController = controller;
    }

    public HeroEventsArgs(int[] levelsOfSkills)
    {
        LevelsOfSkills = levelsOfSkills;
    }


    public HeroEventsArgs(ItemExecuteType type)
    {
        ItemExecuteType = type;
    }

    public HeroEventsArgs(HeroInventory currentHeroInventory, HeroAttributes currentHeroAttributes)
    {
        CurrentHeroInventory = currentHeroInventory;
        CurrentHeroAttributes = currentHeroAttributes;
    }

    public HeroEventsArgs(SkillConfig[] skillsData, HeroAttributes currentHeroAttributes, HeroInventory currentHeroInventory, int[] levelsOfSkills)
    {
        SkillsData = skillsData;
        CurrentHeroAttributes = currentHeroAttributes;
        CurrentHeroInventory = currentHeroInventory;
        LevelsOfSkills = levelsOfSkills;
    }

    public HeroEventsArgs(int SkillId, int SkillLevelId, SkillConfig[] skillsData, HeroAttributes currentHeroAttributes, 
        HeroInventory currentHeroInventory, int[] levelsOfSkills)
    {
        this.SkillId = SkillId;
        this.SkillLevelId = SkillLevelId;
        SkillsData = skillsData;
        CurrentHeroAttributes = currentHeroAttributes;
        CurrentHeroInventory = currentHeroInventory;
        LevelsOfSkills = levelsOfSkills;
    }
}
