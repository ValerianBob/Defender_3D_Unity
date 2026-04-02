using UnityEngine;
using UnityEngine.EventSystems;

public class LevelUpSkillUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int skillId;
    [SerializeField] private int skillLevelId;

    public int SkillId
    {
        get => skillId;
    }

    public int SkillLevelId
    {
        get => skillLevelId;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"You trying to improve Skill :{skillId + 1} to Level :{skillLevelId + 1}");

        UIEvents.OnLevelUpUIHenlder?.Invoke(skillId, skillLevelId);
    }
}
