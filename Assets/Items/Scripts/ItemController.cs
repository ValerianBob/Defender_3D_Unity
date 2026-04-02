using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemConfig ItemData;

    public ItemConfig GetItemData()
    {
        return ItemData;
    }
}
