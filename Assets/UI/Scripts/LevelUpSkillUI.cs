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
        Debug.Log($"Skill {skillId}, Level {skillLevelId}");
    }
}
