using UnityEngine;

public class UIController : MonoBehaviour
{
    private UIAttributes uiAttributes;
    private UIHealthAndMana uiHealthAndMana;
    private UILevelAndXP uiLevelAndXp;
    private UISkills uiSkills;
    private UIInventory uiInventory;

    private void Awake()
    {
        uiAttributes = GetComponent<UIAttributes>();
        uiHealthAndMana = GetComponent<UIHealthAndMana>();
        uiLevelAndXp = GetComponent<UILevelAndXP>();
        uiSkills = GetComponent<UISkills>();
        uiInventory = GetComponent<UIInventory>();
    }

    //private void OnEnable()
    //{
    //    HeroEvents.OnHealthChangeHenlder += ChangeHealthInfo;
    //    HeroEvents.OnManaChangeHendler += ChangeManaInfo;

    //    HeroEvents.OnXpGainHendler += ChangeXpInfo;

    //    HeroEvents.OnLevelUpHendler += ChangeLevelUpInfo;
    //    HeroEvents.OnSkillLevelUpHendler += LevelUpSkillInfo;
    //    HeroEvents.OnHeroSpawnHendler += SetSkillsPanelsInfo;

    //    HeroEvents.OnItemTakeHendler += SetItemsIconsInfo;

    //    HeroEvents.OnGoldGainHendler += SetGoldInfo;
    //}

    //private void OnDisable()
    //{
    //    HeroEvents.OnHealthChangeHenlder -= ChangeHealthInfo;
    //    HeroEvents.OnManaChangeHendler -= ChangeManaInfo;

    //    HeroEvents.OnXpGainHendler -= ChangeXpInfo;

    //    HeroEvents.OnLevelUpHendler -= ChangeLevelUpInfo;
    //    HeroEvents.OnSkillLevelUpHendler -= LevelUpSkillInfo;
    //    HeroEvents.OnHeroSpawnHendler -= SetSkillsPanelsInfo;

    //    HeroEvents.OnItemTakeHendler -= SetItemsIconsInfo;

    //    HeroEvents.OnGoldGainHendler -= SetGoldInfo;
    //}

    //public void ChangeHealthInfo(float currentHealth, float maxHealth)
    //{
    //    uiHealthAndMana.ChangeHealthSliderAndText(currentHealth, maxHealth);
    //}

    //public void ChangeManaInfo(float currentMana, float MaxMana)
    //{
    //    uiHealthAndMana.ChangeManaSliderAndText(currentMana, MaxMana);
    //}

    //public void ChangeXpInfo(float currentXp, float XpForLevelUp)
    //{
    //    uiLevelAndXp.ChangeXpSliderAndText(currentXp, XpForLevelUp);
    //}

    //public void ChangeLevelUpInfo(int[] LevelsOfSkills, HeroAttributes CurrentHeroAttributes)
    //{
    //    uiAttributes.UpdateAttributesInfo(CurrentHeroAttributes);

    //    if (CurrentHeroAttributes.Lv > 1)
    //    {
    //        uiSkills.ChangeSkillsLevelUp(LevelsOfSkills);
    //    }
    //}

    //public void LevelUpSkillInfo(int SkillId, int SkillLevelId, int PointsForLevelUpSkill, int[] HeroLevelsOfSkills)
    //{
    //    uiSkills.LevelUpSkill(SkillId, SkillLevelId, PointsForLevelUpSkill, HeroLevelsOfSkills);
    //}

    //public void SetSkillsPanelsInfo(SkillConfig[] SkillsData)
    //{
    //    uiSkills.SetSkillsPanels(SkillsData);
    //}

    //public void ClearAllSkillsLevelUpsInfo(int[] HeroLevelsOfSkills)
    //{
    //   uiSkills.ClearAllSkillsLevelUps(HeroLevelsOfSkills);
    //}

    //public void SetGoldInfo(int gold)
    //{
    //    uiInventory.SetGold(gold);
    //}

    //public void SetItemsIconsInfo(ItemConfig[] items)
    //{
    //    uiInventory.SetItemsIcons(items);
    //}
}
