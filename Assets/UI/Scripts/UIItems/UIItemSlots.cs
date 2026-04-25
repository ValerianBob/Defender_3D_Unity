using UnityEngine;

public class UIItemSlots : MonoBehaviour
{
    [SerializeField] private int ItemSlotIndex;

    public int GetItemSlotIdex()
    {
        return ItemSlotIndex;
    }
}
