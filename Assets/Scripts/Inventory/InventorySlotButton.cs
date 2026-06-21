using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotButton : MonoBehaviour, ISelectHandler
{
    private Image itemImage;

    private void Start()
    {
        itemImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        InventoryManager.Instance.SetCurrentItem(transform.GetSiblingIndex());
    }

    public void SetSprite(Sprite sprToSet)
    {
        itemImage.sprite = sprToSet;
        itemImage.color = sprToSet == null ? Color.clear : Color.white;
        itemImage.preserveAspect = true;
    }
}
