using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TextHoverColorTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    private const float TEXT_FILL_DURATION_SECONDS = 0.75f;
    [SerializeField] private Graphic originalText;
    [SerializeField] private Color targetColor;
    [SerializeField] private float timeToFill;
    private Color originalColor;

    private void Start()
    {
        // if (originalText is TextMeshProUGUI)
        // {
        //     originalColor = (originalText as TextMeshProUGUI).color;
        // }
        // else if (originalText is Text)
        // {
        //     originalColor = (originalText as Text).color;
        // }
        originalColor = originalText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // if (originalText is TextMeshProUGUI)
        // {
        //     (originalText as TextMeshProUGUI).DOColor(targetColor, TEXT_FILL_DURATION_SECONDS);
        // }
        // else if (originalText is Text)
        // {
        //     (originalText as Text).DOColor(targetColor, TEXT_FILL_DURATION_SECONDS);
        // }
        originalText.DOColor(targetColor, timeToFill);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // if (originalText is TextMeshProUGUI)
        // {
        //     (originalText as TextMeshProUGUI).DOColor(originalColor, TEXT_FILL_DURATION_SECONDS);
        // }
        // else if (originalText is Text)
        // {
        //     (originalText as Text).DOColor(originalColor, TEXT_FILL_DURATION_SECONDS);
        // }
        originalText.DOColor(originalColor, timeToFill);
    }
}
