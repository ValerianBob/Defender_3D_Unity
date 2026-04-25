using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "RageSkill", menuName = "Skills/Rage")]
public class RageSkill : Skill
{
    public float IncreaseAttackSpeed;
    
    public RageSkill(SkillConfig config) : base(config) 
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

        IncreaseAttackSpeed = 0.1f;
    }

    public override void Execute(HeroController CurrentHeroController)
    {
        CurrentHeroController.ChagneMana(false, ManaCost);

        CurrentHeroController.StartCoroutine(Duration(CurrentHeroController));

        Debug.Log($"I used rage skill {ManaCost} {CoolDown}");
    }

    public override void UpdateSkill()
    {
        SkillDuration += 1;
        ManaCost -= 2;
        CoolDown -= 1;

        IncreaseAttackSpeed += 0.1f;
    }

    private IEnumerator Duration(HeroController CurrentHeroController)
    {
        CurrentHeroController.Hero_Attributes.CurrentAttackSpeed -= IncreaseAttackSpeed;

        HeroEventsArgs HeroArgs = new HeroEventsArgs(CurrentHeroController.Hero_Attributes);
        HeroEvents.OnSkillUseHandler?.Invoke(HeroArgs);

        yield return new WaitForSeconds(SkillDuration);

        CurrentHeroController.Hero_Attributes.CurrentAttackSpeed += IncreaseAttackSpeed;

        HeroEventsArgs HeroArgs1 = new HeroEventsArgs(CurrentHeroController.Hero_Attributes);
        HeroEvents.OnSkillUseHandler?.Invoke(HeroArgs1);
    }
}
