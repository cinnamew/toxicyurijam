using DG.Tweening;
using Live2D.Cubism.Framework.Expression;
using Live2D.Cubism.Rendering;
using UnityEngine;

public class LiveSpriteController : MonoBehaviour
{
    private const float FADE_DURATION = 0.25f;
    [SerializeField] private CubismRenderController cubismRenderController;
    [SerializeField] private CubismExpressionController cubismExpressionController;
    [SerializeField] private Animator animator;
    private int expressionListSize;

    private void Start()
    {
        expressionListSize = cubismExpressionController.ExpressionsList.CubismExpressionObjects.Length;
    }

    public void ChangeExpression(int expressionIndex)
    {
        if (expressionIndex > expressionListSize - 1) cubismExpressionController.CurrentExpressionIndex = 0;
        else if (expressionIndex < 0) cubismExpressionController.CurrentExpressionIndex = expressionListSize - 1;
        else cubismExpressionController.CurrentExpressionIndex = expressionIndex;
    }

    private void ChangeModelVisibility(bool show)
    {
        float endValue = show ? 1.0f : 0.0f;
        animator.speed = endValue;
        DOTween.To(() => cubismRenderController.Opacity, x => cubismRenderController.Opacity = x, endValue, FADE_DURATION);
    }

    public void HideModel() => ChangeModelVisibility(false);
    public void ShowModel() => ChangeModelVisibility(true);
    public void HideModelInstant() => cubismRenderController.Opacity = 0;
}
