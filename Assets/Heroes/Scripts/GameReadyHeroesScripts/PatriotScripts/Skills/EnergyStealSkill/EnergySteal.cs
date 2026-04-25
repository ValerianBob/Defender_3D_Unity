using UnityEngine;

[CreateAssetMenu(fileName ="EnergySteal", menuName = "Skills/EnergySteal")]
public class EnergySteal : Skill
{
    public int HealAmount;

    public EnergySteal(SkillConfig config) : base(config)
    {
    }

    public override void InitSkill(string name, string description, int CoolDown, int ManaCost, int SkillDuration)
    {
        CurrentSkillLevel = 0;

        this.SkillName = name;
        this.SkillDescription = description;

        this.CoolDown = CoolDown;
        this.ManaCost = ManaCost;
        this.SkillDuration = SkillDuration;
        isReloading = false;

        HealAmount = 10;
    }

    public override void Execute(HeroController CurrentHeroController)
    {
        CurrentHeroController.ChangeHealth(false, HealAmount);

        Debug.Log("Life steal triggered!");
    }

    public override void UpdateSkill()
    {
        HealAmount += 10;
    }
}
