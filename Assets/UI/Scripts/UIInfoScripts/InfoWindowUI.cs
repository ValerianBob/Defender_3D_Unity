using TMPro;
using UnityEngine;

public class InfoWindowUI : MonoBehaviour
{
    public static InfoWindowUI Instance;

    [SerializeField] private GameObject InfoWindow;

    [SerializeField] private TMP_Text Title;
    [SerializeField] private TMP_Text Description;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(IShowInfo info)
    {
        Title.text = info.GetTitle();
        Description.text = info.GetDescription();

        InfoWindow.SetActive(true);
    }

    public void Hide()
    {
        InfoWindow.SetActive(false);
    }
}
