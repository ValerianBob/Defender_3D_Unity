using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    [SerializeField] private GameObject ShopWindow;

    [SerializeField] private Button ShopButtonToggle;

    private bool isOpened = false;

    private void Start()
    {
        ShopButtonToggle.onClick.AddListener(ToggleShopWindow);
    }

    private void ToggleShopWindow()
    {
        isOpened = !isOpened;

        ShopWindow.SetActive(isOpened);
    }
}
