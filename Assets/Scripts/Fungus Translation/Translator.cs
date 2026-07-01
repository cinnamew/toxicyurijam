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
    [SerializeField] LiveSpriteController adrianne;
    [SerializeField] LiveSpriteController oldMan;

    [SerializeField] TextAsset dialogue;

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

    public void ReadFile()
    {
        string[] lines = dialogue.text.Split('\n');

        for(int i = 0; i < lines.Length; i++)
        {
            ParseCommand(lines[i]);
        }
    }

    public void ParseCommand(string line)
    {
        int spaceIndex = line.IndexOf(' ');
        string firstWord = spaceIndex >= 0 ? line.Substring(0, spaceIndex) : line;
        switch(firstWord.ToLower())
        {
            case "":    //gotta test if this actually catches empty lines tho
                return;
            case "pause":   // need to test
                Wait w = AddCommand<Wait>(currBlock);
                w.SetDuration(float.Parse(line.Split(" ")[1]));
                break;
            case "show":
                LiveSpriteController model = GetModel(line.Split(' ')[1]);
                InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);
                ev.StaticEvent.AddListener(model.ShowModel);
                break;
            case "timeshow":
                lineSplit = line.Split(' ');
                LiveSpriteController m = GetModel(lineSplit[1]);
                float seconds = float.Parse(lineSplit(' ')[2]);
                AddCommand<InvokeEvent>(currBlock).StaticEvent.AddListener(() => m.ShowModelTimed(seconds));
                break;
            case "hide":
                LiveSpriteController m = GetModel(line.Split(' ')[1]);
                AddCommand<InvokeEvent>(currBlock).StaticEvent.AddListener(m.HideModel);
                break;
            case "timehide":
                LiveSpriteController m = GetModel(line.Split(' ')[1]);
                float seconds = float.Parse(line.Split(' ')[2]);
                AddCommand<InvokeEvent>(currBlock).StaticEvent.AddListener(() => m.HideModelTimed(seconds));
                break;
            case "slidein": 
                LiveSpriteController m = GetModel(line.Split(' ')[1]);
                int distance = int.Parse(line.Split(' ')[2]);
                AddCommand<InvokeEvent>(currBlock).StaticEvent.AddListener(() => m.SlideModelInX(distance));
                break;
            case "slideout":
                LiveSpriteController m = GetModel(line.Split(' ')[1]);
                AddCommand<InvokeEvent>(currBlock).StaticEvent.AddListener(m.SlideModelOutX);
                break;
            case "exp":
                LiveSpriteController m = GetModel(line.Split(' ')[1]);
                int expIndex = int.Parse(line.Split(' ')[2]);
                AddCommand<InvokeEvent>(currBlock).StaticEvent.AddListener(() => m.ChangeExpression(expIndex));
                break;
            default:
                break;
        }

        //check for say command

        //else, add as a comment

    }

    public LiveSpriteController GetModel(string modelName)
    {
        switch(modelName.ToLower())
        {
            case "m":
                return mary;
            case "h":
                return hazel;
            case "d":
                return duke;
            case "a":
                return adrianne;
            case "o":
                return oldMan;
            default:
                // silhouette
                return hazel;
        }
    }

}
