using UnityEngine;
using Fungus;
using System.IO;

public class Translator : MonoBehaviour
{
    
    [SerializeField] Flowchart flowchart;
    private Fungus.Block currBlock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currBlock = MakeNewBlock();
    }

    public Block MakeNewBlock(string name="block")
    {
        flowchart.CreateBlock(new Vector2(0,0));
        currBlock.BlockName = name;
    }

    public Command AddCommand<Command>(Block block)
    {
        Command cmd = flowchart.gameObject.AddComponent<Command>();
        cmd.ItemId = flowchart.NextItemId();
        cmd.ParentBlock = currBlock;
        currBlock.CommandList.Add(cmd);
        return cmd;
    }

    public void ReadFile(string fileName)
    {
        string[] lines = File.ReadAllLines(fileName + ".txt");

        for(int i = 0; i < lines.Length; i++)
        {
            ParseCommand(lines[i]);
        }
    }

    public void ParseCommand(string line)
    {
        int spaceIndex = line.IndexOf(' ');
        string firstWord = spaceIndex >= 0 ? line.Substring(0, spaceIndex) : line;
        switch(line)
        {
            case "":    //gotta test if this actually catches empty lines tho
                return;
            case "pause":   // need to test
                Wait w = AddCommand<Wait>(currBlock);
                w._duration = line.Split(" ")[1];
            case default:
                break;
        }

        //check for say command

        //else, add as a comment

    }

    public void MakeNewBlock(string name)
    {
        currBlock = flowchart.CreateBlock(new Vector2(0, 0));
        currBlock.BlockName = name;
    }

}
