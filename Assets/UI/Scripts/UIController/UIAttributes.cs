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

    public void UpdateAttributesInfo(HeroAttributes currentHeroAttributes)
    {
        HeroNameText.text = currentHeroAttributes.HeroName;

        AttackDamgeText.text = currentHeroAttributes.CurrentDamage.ToString();
        MagicDamgeText.text = currentHeroAttributes.CurrentMagicDamage.ToString();

        AttackRangeText.text = currentHeroAttributes.CurrentAttackRange.ToString();

        MoveSpeedText.text = currentHeroAttributes.CurrentMoveSpeed.ToString();
        AttackSpeedText.text = currentHeroAttributes.CurrentAttackSpeed.ToString();
    }

    private void OnHeroSelectWrapper(SkillConfig[] SkillsData, int[] LevelsOfSkills, HeroAttributes currentHeroAttributes,
        HeroInventory heroInventory)
    {
        UpdateAttributesInfo(currentHeroAttributes);
    }

    private void OnLevelUpWrapper(int[] LevelsOfSkills, HeroAttributes currentHeroAttributes)
    {
        UpdateAttributesInfo(currentHeroAttributes);
    }

    private void OnEnable()
    {
        HeroEvents.OnHeroSelectHendler += OnHeroSelectWrapper;

        HeroEvents.OnLevelUpHendler += OnLevelUpWrapper;
    }

    private void OnDisable()
    {
        HeroEvents.OnHeroSelectHendler -= OnHeroSelectWrapper;

        HeroEvents.OnLevelUpHendler -= OnLevelUpWrapper;
    }
}
