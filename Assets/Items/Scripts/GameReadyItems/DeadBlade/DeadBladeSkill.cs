using UnityEngine;

[CreateAssetMenu(menuName = "Items/ItemsSkills/DeadBlade")]
public class DeadBladeSkill : ItemSkill
{
    public override void Execute(HeroEventsArgs HeroArgs)
    {
        HeroArgs.HeroController.ChangeHealth(false, 10);

        Debug.Log("Dead blade make all dead");
    }
}
