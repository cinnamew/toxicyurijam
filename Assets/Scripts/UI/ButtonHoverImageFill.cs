using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonHoverImageFill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float timeToFill;
    [SerializeField] private Image[] fillImages;

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (Image image in fillImages)
        {
            image.DOFillAmount(1, timeToFill);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Image image in fillImages)
        {
            image.DOFillAmount(0, timeToFill);
        }
    }
}
