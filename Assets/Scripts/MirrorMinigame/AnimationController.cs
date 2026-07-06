using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private string triggerName;
    private int triggerHash;

    private void Start()
    {
        triggerHash = Animator.StringToHash(triggerName);
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(TriggerAnimation), 1.0f, Random.Range(1f, 5f));
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(TriggerAnimation));
    }

    private void TriggerAnimation()
    {
        anim.SetTrigger(triggerHash);
    }
}
