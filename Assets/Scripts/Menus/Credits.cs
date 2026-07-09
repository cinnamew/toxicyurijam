using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class Credits : MonoBehaviour
{
    [SerializeField] private VerticalLayoutGroup creditsContainer;
    [SerializeField] private GameObject returnToTitleButton;
    [SerializeField] private Flowchart flowchart;

    private int nextChildIndex;
    private bool finished;

    private void Start()
    {
        foreach (Transform child in creditsContainer.transform) {
            child.gameObject.SetActive(false);
        }

        returnToTitleButton.SetActive(false);
    }

    private void Update()
    {
        if (finished || !(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))) {
            return;
        }

        AdvanceCredits();
    }

    private void AdvanceCredits()
    {
        Transform container = creditsContainer.transform;

        if (nextChildIndex < container.childCount)
        {
            container.GetChild(nextChildIndex).gameObject.SetActive(true);
            nextChildIndex++;
            return;
        }

        if (returnToTitleButton != null) returnToTitleButton.SetActive(true);
        
        flowchart.ExecuteBlock("end of game");

        finished = true;
    }
}
