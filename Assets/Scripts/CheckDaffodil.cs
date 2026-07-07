using Fungus;
using UnityEngine;

public class CheckDaffodil : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    private string[] playerPrefsInventory;


    private void Start()
    {
        playerPrefsInventory = PlayerPrefs.GetString(Globals.INVENTORY).Split(Globals.INV_SEPARATER);
        playerPrefsInventory[0] = "daffodil";
        PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerPrefsInventory));
    }

    public void HasFunnyFlower()
    {
        playerPrefsInventory = PlayerPrefs.GetString(Globals.INVENTORY).Split(Globals.INV_SEPARATER);
        
        // PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerPrefsInventory));
        for (int i = 0; i < playerPrefsInventory.Length; i++)
        {
            if (playerPrefsInventory[i] == "daffodil")
            {
                playerPrefsInventory[i] = "nullobj";
                flowchart.SetBooleanVariable("HasDaffodil", true);
                PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerPrefsInventory));
                return;
            }
        }
    }
}
