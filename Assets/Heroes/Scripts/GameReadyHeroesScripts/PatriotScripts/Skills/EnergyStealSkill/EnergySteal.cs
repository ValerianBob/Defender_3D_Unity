using UnityEngine;

[CreateAssetMenu(fileName ="EnergySteal", menuName = "Skills/EnergySteal")]
public class EnergySteal : Skill
{
    public int HealAmount;

    public EnergySteal(SkillConfig config) : base(config)
    {
    }

    public override void Execute(HeroController CurrentHeroController)
    {
        CurrentHeroController.ChangeHealth(false, HealAmount);

        Debug.Log("Life steal triggered!");
    }
}
