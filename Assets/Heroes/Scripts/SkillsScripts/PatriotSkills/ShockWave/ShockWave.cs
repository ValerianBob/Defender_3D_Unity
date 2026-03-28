using UnityEngine;

[CreateAssetMenu(fileName = "ShockWave", menuName = "Skills/ShockWave")]
public class ShockWave : Skill
{
    public ShockWave(SkillConfig config) : base(config)
    {
    }

    public override void Execute()
    {
        Debug.Log($"ShockWave {Damage} {ManaCost} {CoolDown}");
    }
}
