using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonHoverImageFill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float IMAGE_FILL_DURATION_SECONDS = 0.5f;
    [SerializeField] private Image[] fillImages;

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (Image image in fillImages)
        {
            image.DOFillAmount(1, IMAGE_FILL_DURATION_SECONDS);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Image image in fillImages)
        {
            image.DOFillAmount(0, IMAGE_FILL_DURATION_SECONDS);
        }
    }
}
