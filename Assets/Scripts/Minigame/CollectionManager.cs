using Fungus;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private int totalCollections;
    [SerializeField] private string finishedBlockToRun;
    private int currentCollection = 0;

    public void AddCollection()
    {
        currentCollection++;
    }

    public void CheckCollectionStatus()
    {
        if (currentCollection >= totalCollections)
        {
            flowchart.ExecuteBlock(finishedBlockToRun);
        }
    }
}
