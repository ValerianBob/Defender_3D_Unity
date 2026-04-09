using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragAndDropItems : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Transform _originalParent;
    private Vector2 _originalPosition;

    private Canvas _canvas;
    private CanvasGroup _canvasGroup;

    private RectTransform _rectTransform;

    [SerializeField] private int ItemId;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();

        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = transform.parent;
        _originalPosition = _rectTransform.anchoredPosition;

        transform.SetParent(_canvas.transform);
        _canvasGroup.blocksRaycasts = false;

        Debug.Log("I started drag item");
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

        Debug.Log("I dragging items");
    }

    //TODO
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(_originalParent);
        _rectTransform.anchoredPosition = _originalPosition;

        _canvasGroup.blocksRaycasts = true;

        GameObject target = eventData.pointerCurrentRaycast.gameObject;

        UIItemSlots slot = target != null ? target.GetComponentInParent<UIItemSlots>() : null;

        if (target == null)
        {
            UIEvents.OnItemDropUIHandler?.Invoke(ItemId);

            return;
        }
        else if (slot != null)
        {
            UIEvents.OnItemSwapUIHandler?.Invoke(ItemId, slot.GetItemSlotIdex());

            Debug.Log($"Trying swap with slot :{slot.GetItemSlotIdex()}");

            return;
        }
        else
        {
            Debug.Log($"Can't drop on :{target.name}");
        }
    }
}
