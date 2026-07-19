using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class GetAllChapters : MonoBehaviour, IPointerClickHandler
{
    private const int TOTAL_PATS = 10;
    private readonly WaitForSeconds waitForSeconds = new(10.0f);
    private int currentHeadPats = 0;
    private bool isPattingTimerStarted = false;
    private BoxCollider2D headPatCollider;


    private void Start()
    {
        headPatCollider = GetComponent<BoxCollider2D>();
        if (PlayerPrefs.GetInt(Globals.SCENE_SEEN, 0) == 7)
        {
            headPatCollider.enabled = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentHeadPats++;
        if (currentHeadPats >= TOTAL_PATS)
        {
            UnlockAllChapters();
            if (isPattingTimerStarted) StopCoroutine(nameof(Countdown));
        }
        if (currentHeadPats < TOTAL_PATS && !isPattingTimerStarted)
        {
            StartCoroutine(nameof(Countdown));
            isPattingTimerStarted = true;
        }
    }

    private IEnumerator Countdown()
    {
        yield return waitForSeconds;
        if (currentHeadPats >= TOTAL_PATS)
        {
            UnlockAllChapters();
        }
        else
        {
            currentHeadPats = 0;
            isPattingTimerStarted = false;
        }
        yield return new WaitForEndOfFrame();
    }

    private void UnlockAllChapters()
    {
        PlayerPrefs.SetInt(Globals.SCENE_SEEN, 7);
        headPatCollider.enabled = false;
    }
}
