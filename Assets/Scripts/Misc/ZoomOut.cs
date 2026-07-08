using DG.Tweening;
using UnityEngine;

public class ZoomOut : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera targetCamera;

    [Header("Zoomed-in framing")]
    [SerializeField] private float zoomedInX = 3f;
    [SerializeField] private float zoomedInY = 2f;
    [SerializeField] private float zoomedInSize = 2.5f;

    [Header("meow")]
    [SerializeField] private float zoomOutDurationInSeconds = 1f;
    [SerializeField] private Ease easing = Ease.InOutSine;

    private Vector3 defaultCameraPosition;
    private float defaultOrthographicSize;
    private Sequence activeTween;


    private void Start()
    {
        CacheDefaultFraming();
        ZoomCamIn();
    }

    private void CacheDefaultFraming()
    {
        defaultCameraPosition = targetCamera.transform.position;
        defaultOrthographicSize = targetCamera.orthographicSize;
    }

    public void ZoomCamIn()
    {
        Vector3 destination = new Vector3(zoomedInX, zoomedInY, defaultCameraPosition.z);
        AnimateTo(destination, zoomedInSize, 0);
    }

    public void ZoomCamOut()
    {
        AnimateTo(defaultCameraPosition, defaultOrthographicSize, zoomOutDurationInSeconds);
    }

    private void AnimateTo(Vector3 position, float orthographicSize, float duration)
    {
        activeTween = DOTween.Sequence()
            .Join(targetCamera.transform.DOMove(position, duration))
            .Join(targetCamera.DOOrthoSize(orthographicSize, duration))
            .SetEase(easing);
    }

}
