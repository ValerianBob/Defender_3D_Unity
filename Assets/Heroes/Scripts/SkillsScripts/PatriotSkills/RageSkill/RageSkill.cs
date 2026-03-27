using UnityEngine;

[CreateAssetMenu(fileName = "RageSkill", menuName = "Skills/Rage")]
public class RageSkill : Skill
{
    public RageSkill(SkillConfig config) : base(config) { }

    public override void Execute()
    {
        Debug.Log("I used rage skill");
    }
}
