using UnityEngine;

[CreateAssetMenu(fileName ="EnergySteal", menuName = "Skills/EnergySteal")]
public class EnergySteal : Skill
{
    public EnergySteal(SkillConfig config) : base(config)
    {
    }

    public override void Execute()
    {
        Debug.Log($"Stealing energy {Damage} {ManaCost} {CoolDown}");
    }
}
