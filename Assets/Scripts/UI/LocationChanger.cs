using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;
using System.Collections;
using UnityEngine.Localization.Tables;

public class LocationChanger : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (text != null)
        {
            ChangeLocationText(text.text);
            //Debug.Log("meow");
        }
    }

    public void ChangeLocationText(string newLoc)
    {
        StartCoroutine(LocalizeLocation(newLoc));
    }

    public IEnumerator LocalizeLocation(string newLoc)
    {
        string key = "Location " + newLoc;
        var tableOperation = LocalizationSettings.StringDatabase.GetTableAsync("NonDialogueText");
        yield return tableOperation;

        StringTable table = tableOperation.Result;
        StringTableEntry entry = table.GetEntry(key);
        if (entry == null)
        {
            Debug.Log("localization key for " + newLoc + " doesn't exist :(((");
            text.text = "?";
            yield break;
        }
        //LocalizationSettings.StringDatabase.GetLocalizedString("NonDialogueText", key);
        string newText = entry.GetLocalizedString();
        text.text = newText;
    }
}
