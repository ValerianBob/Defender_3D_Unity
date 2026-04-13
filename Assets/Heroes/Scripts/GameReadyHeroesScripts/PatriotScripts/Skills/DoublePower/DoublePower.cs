using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DoublePower", menuName = "Skills/DoublePower")]
public class DoublePower : Skill
{
    public int IncreaseAttackDamage;
    public DoublePower(SkillConfig config) : base(config)
    {
    }

    public override void Execute(HeroController CurrentHeroController)
    {
        CurrentHeroController.ChagneMana(false, ManaCost);

        CurrentHeroController.StartCoroutine(Duration(CurrentHeroController));

        Debug.Log($"I used Double Power skill {ManaCost} {CoolDown}");
    }

    private IEnumerator Duration(HeroController CurrentHeroController)
    {
        CurrentHeroController.Hero_Attributes.CurrentDamage += IncreaseAttackDamage;

        HeroEventsArgs HeroArgs = new HeroEventsArgs(CurrentHeroController.Hero_Attributes);
        HeroEvents.OnSkillUseHandler?.Invoke(HeroArgs);

        yield return new WaitForSeconds(SkillDuration);

        CurrentHeroController.Hero_Attributes.CurrentDamage -= IncreaseAttackDamage;

        HeroEventsArgs HeroArgs1 = new HeroEventsArgs(CurrentHeroController.Hero_Attributes);
        HeroEvents.OnSkillUseHandler?.Invoke(HeroArgs1);
    }
}
