using UnityEngine;

public class MirrorGameManager : MonoBehaviour
{
    private const int PHASE_ONE_GOAL = 5;

    [SerializeField] private GameObject[] phaseOneObjects;
    [SerializeField] private GameObject[] phaseTwoObjects;
    [SerializeField] private GameObject[] mirrorCrackStages;
    private int currentCrackStage = 0;
    private int phaseOneClicks = 0;

    public void CrackMirror()
    {
        if (mirrorCrackStages.Length == 0 || currentCrackStage > mirrorCrackStages.Length) return;
        mirrorCrackStages[currentCrackStage].SetActive(true);
        currentCrackStage++;
    }

    public void PhaseOneItemClicked()
    {
        phaseOneClicks++;
        if (phaseOneClicks >= PHASE_ONE_GOAL)
        {
            foreach (GameObject g in phaseOneObjects) 
            {
                g.SetActive(false);
            }

            foreach (GameObject g in phaseTwoObjects)
            {
                g.SetActive(true);
            }
        }
    }
}
