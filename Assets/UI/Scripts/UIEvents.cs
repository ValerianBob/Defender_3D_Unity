using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public delegate void OnLevelUpUI(int SkillId, int LevelOfSkill);
    public static OnLevelUpUI OnLevelUpUIHenlder;
}
