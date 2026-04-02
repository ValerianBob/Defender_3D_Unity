using UnityEngine;

[CreateAssetMenu(fileName = "DoublePower", menuName = "Skills/DoublePower")]
public class DoublePower : Skill
{
    public DoublePower(SkillConfig config) : base(config)
    {
    }

    public override void Execute()
    {
        Debug.Log($"Double Power {Damage} {ManaCost} {CoolDown}");
    }
}
