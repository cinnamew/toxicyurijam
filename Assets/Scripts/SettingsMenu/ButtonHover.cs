using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private const float IMAGE_FILL_DURATION_SECONDS = 0.5f;
    private const float TEXT_FILL_DURATION_SECONDS = 0.75f;

    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI exitText;
    private Color exitTextOriginalColor;

    private void Start()
    {
        exitTextOriginalColor = exitText.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        fillImage.DOFillAmount(1, IMAGE_FILL_DURATION_SECONDS);
        exitText.DOColor(Color.white, TEXT_FILL_DURATION_SECONDS);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        fillImage.DOFillAmount(0, IMAGE_FILL_DURATION_SECONDS);
        exitText.DOColor(exitTextOriginalColor, TEXT_FILL_DURATION_SECONDS);
    }
}
