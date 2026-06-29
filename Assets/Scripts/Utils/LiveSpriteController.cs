using DG.Tweening;
using Live2D.Cubism.Framework.Expression;
using Live2D.Cubism.Rendering;
using UnityEngine;

public class LiveSpriteController : MonoBehaviour
{
    private const float DEFAULT_FADE_DURATION = 0.25f;
    private const float DEFAULT_SLIDE_DURATION = 0.75f;

    [SerializeField] private CubismRenderController cubismRenderController;
    [SerializeField] private CubismExpressionController cubismExpressionController;
    [SerializeField] private Animator animator;
    private int expressionListSize;
    private Vector2 originalLocation;


    private void Start()
    {
        expressionListSize = cubismExpressionController.ExpressionsList.CubismExpressionObjects.Length;
        originalLocation = transform.position;
    }

    private void ChangeModelVisibility(bool show, float duration)
    {
        float endValue = show ? 1.0f : 0.0f;
        animator.speed = endValue;
        DOTween.To(() => cubismRenderController.Opacity, x => cubismRenderController.Opacity = x, endValue, duration);
    }

    public void ChangeExpression(int expressionIndex)
    {
        if (expressionIndex > expressionListSize - 1) cubismExpressionController.CurrentExpressionIndex = 0;
        else if (expressionIndex < 0) cubismExpressionController.CurrentExpressionIndex = expressionListSize - 1;
        else cubismExpressionController.CurrentExpressionIndex = expressionIndex;
    }

    public void SlideModelInX(int distance) => transform.DOMoveX(originalLocation.x + distance, DEFAULT_SLIDE_DURATION);

    public void SlideModelOutX() => transform.DOMoveX(originalLocation.x, DEFAULT_SLIDE_DURATION);

    public void HideModel() => ChangeModelVisibility(false, DEFAULT_FADE_DURATION);

    public void ShowModel() => ChangeModelVisibility(true, DEFAULT_FADE_DURATION);

    public void HideModelTimed(float duration) => ChangeModelVisibility(false, duration);

    public void ShowModelTimed(float duration) => ChangeModelVisibility(true, duration);

    public void HideModelInstant() => cubismRenderController.Opacity = 0;
}
