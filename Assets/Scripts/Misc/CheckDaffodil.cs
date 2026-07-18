using Fungus;
using UnityEngine;

public class CheckDaffodil : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    private string[] playerInv;

    private void Start()
    {
        // uncomment below to force daffodil addition
        // playerPrefsInventory = PlayerPrefs.GetString(Globals.INVENTORY).Split(Globals.INV_SEPARATER);
        // playerPrefsInventory[0] = "daffodil";
        // PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerPrefsInventory));

        // uncomment below to force "had daffodil"
        // PlayerPrefs.SetInt("HadDaffy", 1);
    }

    public void HasFunnyFlower()
    {
        playerInv = PlayerPrefs.GetString(Globals.INVENTORY).Split(Globals.INV_SEPARATER);
        
        // PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerPrefsInventory));
        for (int i = 0; i < playerInv.Length; i++)
        {
            if (playerInv[i] == "daffodil")
            {
                flowchart.SetBooleanVariable("HasDaffodil", true);
                PlayerPrefs.SetInt("HadDaffy", 1);
                PlayerPrefs.SetInt("GaveDaffy", 0);
                RemoveFunnyFlower();
                return;
            }
        }
    }

    public void GiveFunnyFlower()
    {
        PlayerPrefs.SetInt("GaveDaffy", 1);
    }

    public void HadFunnyFlower()
    {
        if (PlayerPrefs.GetInt("HadDaffy") == 1)
        {
            flowchart.SetBooleanVariable("HadDaffodil", true);
            return;
        }
    }

    public void RemoveFunnyFlower()
    {
        for (int i = 0; i < playerInv.Length; i++)
        {
            if (playerInv[i] == "daffodil")
            {
                playerInv[i] = "nullobj";
                PlayerPrefs.SetString(Globals.INVENTORY, string.Join(Globals.INV_SEPARATER, playerInv));
                GiveFunnyFlower();
                return;
            }
        }
        Debug.Log("you have no funny flower.. what're u removing chief");
    }

    public void BeMean()
    {
        // no flower
        PlayerPrefs.SetInt("GaveDaffy", 0);
    }

    public void GaveFunnyFlower()
    {
        flowchart.SetBooleanVariable("gaveDaffodil", PlayerPrefs.GetInt("GaveDaffy") == 1);
    }
}
