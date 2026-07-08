using System.Linq;
using Fungus;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private int totalCollections;
    [SerializeField] private string finishedBlockToRun;
    [SerializeField] private BoxCollider2D[] optionalCollects;
    private GameObject[] uniqueCollections;
    private int currentCollection = 0;


    private void Start()
    {
        uniqueCollections = new GameObject[totalCollections];
    }

    public void AddCollection(GameObject obj)
    {
        if (uniqueCollections.Contains(obj)) return;
        uniqueCollections[currentCollection] = obj;
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
