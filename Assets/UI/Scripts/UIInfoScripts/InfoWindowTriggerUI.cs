using UnityEngine;
using UnityEngine.EventSystems;

public class InfoWindowTriggerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private IShowInfo information;

    public void Initialize(IShowInfo info)
    {
        information = info;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (information != null)
        {
            InfoWindowUI.Instance.Show(information);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InfoWindowUI.Instance.Hide();
    }
}
