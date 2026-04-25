using TMPro;
using UnityEngine;

public class UIAttributes : MonoBehaviour
{
    [SerializeField] private TMP_Text HeroNameText;

    [SerializeField] private TMP_Text AttackDamgeText;
    [SerializeField] private TMP_Text MagicDamgeText;

    [SerializeField] private TMP_Text AttackRangeText;

    [SerializeField] private TMP_Text MoveSpeedText;
    [SerializeField] private TMP_Text AttackSpeedText;

    [SerializeField] private InfoWindowTriggerUI[] InfoWindowTrigger;

    private void UpdateAttributesInfo(HeroEventsArgs HeroArgs)
    {
        HeroNameText.text = HeroArgs.CurrentHeroAttributes.HeroName;

        AttackDamgeText.text = HeroArgs.CurrentHeroAttributes.CurrentDamage.ToString();
        MagicDamgeText.text = HeroArgs.CurrentHeroAttributes.CurrentMagicDamage.ToString();

        AttackRangeText.text = HeroArgs.CurrentHeroAttributes.CurrentAttackRange.ToString();

        MoveSpeedText.text = HeroArgs.CurrentHeroAttributes.CurrentMoveSpeed.ToString();
        AttackSpeedText.text = HeroArgs.CurrentHeroAttributes.CurrentAttackSpeed.ToString();

        foreach(var item in InfoWindowTrigger)
        {
            item.Initialize(HeroArgs.CurrentHeroAttributes);
        }
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHandler += UpdateAttributesInfo;

        HeroEvents.OnLevelUpHandler += UpdateAttributesInfo;

        HeroEvents.OnItemTakeHandler += UpdateAttributesInfo;

        HeroEvents.OnItemDropHandler += UpdateAttributesInfo;

        HeroEvents.OnSkillUseHandler += UpdateAttributesInfo;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHandler -= UpdateAttributesInfo;

        HeroEvents.OnLevelUpHandler -= UpdateAttributesInfo;

        HeroEvents.OnItemTakeHandler -= UpdateAttributesInfo;

        HeroEvents.OnItemDropHandler -= UpdateAttributesInfo;

        HeroEvents.OnSkillUseHandler -= UpdateAttributesInfo;
    }
}
