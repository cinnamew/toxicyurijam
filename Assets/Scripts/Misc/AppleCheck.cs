using UnityEngine;

public class AppleCheck : MonoBehaviour
{
    private void Start() => gameObject.SetActive(AchivementManager.Instance.AllThreeEndings());
}
