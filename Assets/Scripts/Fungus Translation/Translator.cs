using UnityEngine;
using Fungus;
using System.IO;

public class Translator : MonoBehaviour
{
    
    [SerializeField] Flowchart flowchart;
    private Fungus.Block currBlock;

    [SerializeField] LiveSpriteController mary;
    [SerializeField] LiveSpriteController hazel;
    [SerializeField] LiveSpriteController duke;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currBlock = MakeNewBlock();
    }

    public Block MakeNewBlock(string name="block")
    {
        currBlock = flowchart.CreateBlock(new Vector2(0,0));
        currBlock.BlockName = name;
        return currBlock;
    }

    public T AddCommand<T>(Block block) where T : Command
    {
        T cmd = flowchart.gameObject.AddComponent<T>();
        cmd.ItemId = flowchart.NextItemId();
        cmd.ParentBlock = currBlock;
        currBlock.CommandList.Add(cmd);
        return cmd;
    }

    public void ReadFile(string fileName)
    {
        string[] lines; //File.ReadAllLines(fileName + ".txt");

        for(int i = 0; i < lines.Length; i++)
        {
            ParseCommand(lines[i]);
        }
    }

    public void ParseCommand(string line)
    {
        int spaceIndex = line.IndexOf(' ');
        string firstWord = spaceIndex >= 0 ? line.Substring(0, spaceIndex) : line;
        switch(line.ToLower())
        {
            case "":    //gotta test if this actually catches empty lines tho
                return;
            case "pause":   // need to test
                Wait w = AddCommand<Wait>(currBlock);
                w.SetDuration((float)line.Split(" ")[1]);
                break;
            case "show":
                LiveSpriteController model = GetModel(line.Split(' ')[1]);
                InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);

            default:
                break;
        }

        //check for say command

        //else, add as a comment

    }

}
