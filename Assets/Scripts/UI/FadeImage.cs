using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeImage : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2f;

    private Image image;

    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void FadeOut()
    {
        image.DOFade(0, fadeDuration);
    }
}
