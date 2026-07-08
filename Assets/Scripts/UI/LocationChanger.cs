using UnityEngine;
using TMPro;

public class LocationChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void ChangeLocationText(string newLoc)
    {
        text.text = newLoc;
    }
}
