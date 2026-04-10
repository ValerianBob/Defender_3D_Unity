using UnityEngine;

[CreateAssetMenu(menuName = "Items/ItemsSkills/MjolnirSkill")]
public class MjolnirSkill : ItemSkill
{
    public override void Execute(HeroEventsArgs HeroArgs)
    {
        Debug.Log("Mjolnir make thounder");
    }
}
