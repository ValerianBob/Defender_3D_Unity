public class UIEvents
{
    public delegate void OnLevelUpUI(int SkillId, int LevelOfSkill);
    public static OnLevelUpUI OnLevelUpUIHandler;

    public delegate void OnItemDropUI(int ItemIdToDrop);
    public static OnItemDropUI OnItemDropUIHandler;

    public delegate void OnItemSwapUI(int DraggedItemId, int DroppedOnItemId);
    public static OnItemSwapUI OnItemSwapUIHandler;
}
