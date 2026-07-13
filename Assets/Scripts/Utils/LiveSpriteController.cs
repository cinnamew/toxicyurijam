using DG.Tweening;
using Live2D.Cubism.Framework.Expression;
using Live2D.Cubism.Rendering;
using UnityEngine;

public class LiveSpriteController : MonoBehaviour
{
    private const float DEFAULT_FADE_DURATION = 0.25f;
    private const float DEFAULT_SLIDE_DURATION = 1.0f;
    private const float JUMP_FORCE = 0.1f;
    private const int NUM_JUMPS = 1;
    private const float JUMP_DURATION = 0.4f;

    [SerializeField] private CubismRenderController cubismRenderController;
    [SerializeField] private CubismExpressionController cubismExpressionController;
    [SerializeField] private Animator animator;
    private int expressionListSize;
    private Vector2 originalLocation;
    private Tween jumpTween;
    private DialogueBoxUtilityHandler dialogueBoxUtilityHandler;


    private void Start()
    {
        expressionListSize = cubismExpressionController.ExpressionsList.CubismExpressionObjects.Length;
        originalLocation = transform.position;
        jumpTween = transform.DOJump(transform.position, JUMP_FORCE, NUM_JUMPS, JUMP_DURATION).SetAutoKill(false);
        dialogueBoxUtilityHandler = FindAnyObjectByType<DialogueBoxUtilityHandler>();
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

    public void SetModelPosition(Transform newPos) => transform.position = newPos.position;

    public void FlipModelX() => transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

    public void JumpModel()
    {   
        // bandaid fix
        // if (dialogueBoxUtilityHandler != null && dialogueBoxUtilityHandler.isSkipping) return;

        jumpTween.Kill();
        transform.position = new Vector3(transform.position.x, originalLocation.y, transform.position.z);
        jumpTween = transform.DOJump(new Vector3(transform.position.x, originalLocation.y, transform.position.z), JUMP_FORCE, NUM_JUMPS, JUMP_DURATION).SetAutoKill(false);
        jumpTween.Restart();
    }

    // might change this to SlideModelX for sliding in both directions rather than relying on original position - ex. Mary is originally on left, moves right, then calls SlideModelOutX. Where is original position?
    public void SlideModelInX(int distance)
    {
        transform.position = new(originalLocation.x - distance, transform.position.y, transform.position.z);
        transform.DOMoveX(transform.position.x + distance, DEFAULT_SLIDE_DURATION);
    }

    public void SlideModelOutX()
    {
        if (transform.position.x > 0) SlideModelX(15);
        if (transform.position.x < 0) SlideModelX(-15);
    }

    public void SlideModelX(int distance) => transform.DOMoveX(originalLocation.x + distance, DEFAULT_SLIDE_DURATION).SetEase(Ease.InSine);

    public void HideModel() => ChangeModelVisibility(false, DEFAULT_FADE_DURATION);

    public void ShowModel() => ChangeModelVisibility(true, DEFAULT_FADE_DURATION);

    public void HideModelTimed(float duration) => ChangeModelVisibility(false, duration);

    public void ShowModelTimed(float duration) => ChangeModelVisibility(true, duration);

    public void HideModelInstant() => cubismRenderController.Opacity = 0;
}
