using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class TextHoverColorTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField] private Graphic originalText;
    [SerializeField] private Color targetColor;
    [SerializeField] private float timeToFill;
    private Color originalColor;

    private void Start()
    {
        originalColor = originalText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        originalText.DOColor(targetColor, timeToFill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        originalText.DOColor(originalColor, timeToFill);
    }
}
