using Fungus;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private int totalCollections;
    [SerializeField] private string finishedBlockToRun;
    [SerializeField] private BoxCollider2D[] optionalCollects;
    private int currentCollection = 0;

    public void AddCollection()
    {
        currentCollection++;
    }

    public void CheckCollectionStatus()
    {
        if (currentCollection >= totalCollections)
        {
            foreach (BoxCollider2D b in optionalCollects)
            {
                b.enabled = false;
            }
            flowchart.ExecuteBlock(finishedBlockToRun);
        }
    }
}
