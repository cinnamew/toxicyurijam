using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Fungus;
using UnityEngine.Localization.Tables;

// this is copied from mimco, needs to change
public class LocaleSwitcher : MonoBehaviour
{
    [SerializeField] int currLocaleID = 0;
    [SerializeField] Flowchart f;

    int MAX_LOCALE;  //# of non-english locales
    private bool active = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;
        MAX_LOCALE = LocalizationSettings.AvailableLocales.Locales.Count;
        //print(MAX_LOCALE);
        currLocaleID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(0);

        // assume the locale must exist

        if (f != null)
        {
            f.ExecuteBlock(LocalizationSettings.StringDatabase.GetLocalizedString("NonDialogueText", "locale"));

        }
    }

    public void ChangeLocale(int add)
    {
        if (active) return;
        currLocaleID += add;
        if (currLocaleID < 0) currLocaleID = MAX_LOCALE;
        if (currLocaleID > MAX_LOCALE - 1) currLocaleID = 0;
        StartCoroutine(SetLocale(currLocaleID));
    }

    IEnumerator SetLocale(int localeID)
    {
        //ids: en 0, pt-br 1, uk 2, ru 3, zh-TW 4, fr 5
        active = true;
        yield return LocalizationSettings.InitializationOperation;

        //change this
        var tableOperation = LocalizationSettings.StringDatabase.GetTableAsync("NonDialogueText");
        yield return tableOperation;

        StringTable table = tableOperation.Result;
        StringTableEntry entry = table.GetEntry("exists");
        
        if (string.IsNullOrEmpty(entry.Value))
        {
            Debug.Log("language " + localeID + " doesn't exist :(((");
            yield break;
        }

        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        PlayerPrefs.SetInt("LocaleKey", currLocaleID);
        active = false;
    }
}
