using UnityEngine;
using UnityEngine.Events;
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

    [SerializeField] Character maryChar;
    [SerializeField] Character hazelChar;
    [SerializeField] Character dukeChar;
    [SerializeField] Character adrianneChar;
    [SerializeField] Character oldManChar;

    [SerializeField] TextAsset dialogue;

#if UNITY_EDITOR
    [ContextMenu("Generate Flowchart")]
    public void GenerateFlowchart()
    {
        currBlock = MakeNewBlock();
        ReadFile();

        UnityEditor.EditorUtility.SetDirty(flowchart);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
    }
#endif

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

#if UNITY_EDITOR
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
                {
                    LiveSpriteController m = GetModel(line.Split(' ')[1]);
                    WireVoid(AddCommand<InvokeEvent>(currBlock), m.ShowModel);
                }
                break;
            case "timeshow":
                {
                    LiveSpriteController m = GetModel(line.Split(' ')[1]);
                    float seconds = float.Parse(line.Split(' ')[2]);
                    WireFloat(AddCommand<InvokeEvent>(currBlock), m.ShowModelTimed, seconds);
                }
                break;
            case "hide":
                {
                    LiveSpriteController m = GetModel(line.Split(' ')[1]);
                    WireVoid(AddCommand<InvokeEvent>(currBlock), m.HideModel);
                }
                break;
            case "timehide":
                {
                    LiveSpriteController m = GetModel(line.Split(' ')[1]);
                    float seconds = float.Parse(line.Split(' ')[2]);
                    WireFloat(AddCommand<InvokeEvent>(currBlock), m.HideModelTimed, seconds);
                }
                break;
            case "slidein":
                {
                    LiveSpriteController m = GetModel(line.Split(' ')[1]);
                    int distance = int.Parse(line.Split(' ')[2]);
                    WireInt(AddCommand<InvokeEvent>(currBlock), m.SlideModelInX, distance);
                }
                break;
            case "slideout":
                {
                    LiveSpriteController m = GetModel(line.Split(' ')[1]);
                    WireVoid(AddCommand<InvokeEvent>(currBlock), m.SlideModelOutX);
                }
                break;
            case "exp":
                {
                    LiveSpriteController m = GetModel(line.Split(' ')[1]);
                    int expIndex = int.Parse(line.Split(' ')[2]);
                    WireInt(AddCommand<InvokeEvent>(currBlock), m.ChangeExpression, expIndex);
                }
                break;
            case "rename":
                {
                    string[] parts = line.Split(new char[] { ' ' }, 3);
                    Character c = GetCharacter(parts[1]);
                    WireString(AddCommand<InvokeEvent>(currBlock), c.SetStandardText, parts[2]);
                }
                break;
            case "block":
                string blockName = line.Split(' ')[1];
                currBlock = MakeNewBlock(blockName);
                break;
            default:
                int firstQuote = line.IndexOf('"');
                Character speaker = firstQuote > 0 ? GetCharacter(firstWord) : null;
                if (firstQuote == 0 || speaker != null)
                {
                    // crop leading and trailing "
                    string dialogue = line.Substring(firstQuote + 1, line.Length - firstQuote - 2);
                    Say s = AddCommand<Say>(currBlock);
                    s._Character = speaker;
                    s.SetStandardText(dialogue);
                }
                else
                {
                    AddCommand<Comment>(currBlock).CommentText = line;
                }
                break;
        }
    }

    // persistency
    void WireVoid(InvokeEvent ev, UnityAction call)
    {
        UnityEditor.Events.UnityEventTools.AddPersistentListener(ev.StaticEvent, call);
    }

    void WireFloat(InvokeEvent ev, UnityAction<float> call, float arg)
    {
        UnityEditor.Events.UnityEventTools.AddFloatPersistentListener(ev.StaticEvent, call, arg);
    }

    void WireInt(InvokeEvent ev, UnityAction<int> call, int arg)
    {
        UnityEditor.Events.UnityEventTools.AddIntPersistentListener(ev.StaticEvent, call, arg);
    }

    void WireString(InvokeEvent ev, UnityAction<string> call, string arg)
    {
        UnityEditor.Events.UnityEventTools.AddStringPersistentListener(ev.StaticEvent, call, arg);
    }
#endif

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
                // silhouettes: m1, m2, f1
                return null;
        }
    }

    public Character GetCharacter(string key)
    {
        switch(key.ToLower())
        {
            case "m":
                return maryChar;
            case "h":
                return hazelChar;
            case "d":
                return dukeChar;
            case "a":
                return adrianneChar;
            case "o":
                return oldManChar;
            default:    // TODO: ??? character
                return null;
        }
    }

}
