using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Fungus;
using UnityEngine.Localization.Tables;

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
        Debug.Log("your max locale is " + MAX_LOCALE);

        currLocaleID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(0);

        // assume the locale must exist

        if (f != null)
        {
            f.ExecuteBlock(LocalizationSettings.StringDatabase.GetLocalizedString("NonDialogueText", "locale"));

        }
    }

    public void NextLocale()
    {
        ChangeLocale(1);
    }

    public void PrevLocale()
    {
        ChangeLocale(-1);
    }

    public void ChangeLocale(int add)
    {
        if (active) return;
        int prev = currLocaleID;
        currLocaleID += add;
        if (currLocaleID > MAX_LOCALE - 1) currLocaleID = 0;
        if (currLocaleID < 0) currLocaleID = MAX_LOCALE - 1;
        //Debug.Log("going to try switching to locale " + currLocaleID);
        StartCoroutine(SetLocale(currLocaleID, prev));


        IEnumerator SetLocale(int localeID, int prevLocaleID)
        {
            active = true;
            yield return LocalizationSettings.InitializationOperation;

            //check
            var newLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
            var tableOperation = LocalizationSettings.StringDatabase.GetTableAsync("NonDialogueText", newLocale);
            yield return tableOperation;

            StringTable table = tableOperation.Result;
            if (table == null)
            {
                Debug.Log("language " + localeID + " table doesn't exist :(((");
                active = false;
                ChangeLocale(add);
                yield break;
            }
            StringTableEntry entry = table.GetEntry("exists");

            if (entry == null || string.IsNullOrEmpty(entry.Value))
            {
                Debug.Log("language " + localeID + " exists entry doesn't exist :(((");
                active = false;
                ChangeLocale(add);
                yield break;
            }

            //Debug.Log(entry.Value);

            LocalizationSettings.SelectedLocale = newLocale;
            PlayerPrefs.SetInt("LocaleKey", currLocaleID);

            Debug.Log("hello i've switched to " + LocalizationSettings.StringDatabase.GetLocalizedString("NonDialogueText", "locale"));
            active = false;
        }

    }


}
