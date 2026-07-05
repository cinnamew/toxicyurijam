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
    [SerializeField] Character m1Char;
    [SerializeField] Character m2Char;
    [SerializeField] Character f1Char;

    [SerializeField] Stage stage;

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
        Vector2 offset = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
        currBlock = flowchart.CreateBlock(offset);
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
            case "":
                return;
            case "pause":   // need to test
                Wait w = AddCommand<Wait>(currBlock);
                w.SetDuration(float.Parse(line.Split(" ")[1]));
                break;
            case "show":
                {
                    string[] t = line.Split(' ');
                    int expression = int.Parse(t[2]);
                    LiveSpriteController m = GetModel(t[1]);
                    if (m != null)   // Live2D
                    {
                        InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);
                        ev.Description = line;
                        WireInt(ev, m.ChangeExpression, expression);
                        WireVoid(ev, m.ShowModel);
                    }
                    else             // non-Live2D
                    {
                        Character c = GetCharacter(t[1]);
                        if (c == null) break;
                        Portrait p = AddCommand<Portrait>(currBlock);
                        p._Character = c;
                        p.Display = DisplayType.Show;
                        p._Portrait = PortraitAt(c, expression);
                        p._Stage = stage;
                        p.ToPosition = StagePos(HomePos(c));
                    }
                }
                break;
            case "timeshow":
                {
                    string[] t = line.Split(' ');
                    int expression = int.Parse(t[2]);
                    float seconds = float.Parse(t[3]);
                    LiveSpriteController m = GetModel(t[1]);
                    if (m != null)
                    {
                        InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);
                        ev.Description = line;
                        WireInt(ev, m.ChangeExpression, expression);
                        WireFloat(ev, m.ShowModelTimed, seconds);
                    }
                    else
                    {
                        Character c = GetCharacter(t[1]);
                        if (c == null) break;
                        Portrait p = AddCommand<Portrait>(currBlock);
                        p._Character = c;
                        p.Display = DisplayType.Show;
                        p._Portrait = PortraitAt(c, expression);
                        p._Stage = stage;
                        p.ToPosition = StagePos(HomePos(c));
                        p.UseDefaultSettings = false;
                        p.FadeDuration = seconds;
                    }
                }
                break;
            case "hide":
                {
                    string[] t = line.Split(' ');
                    LiveSpriteController m = GetModel(t[1]);
                    if (m != null)
                    {
                        InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);
                        ev.Description = line;
                        WireVoid(ev, m.HideModel);
                    }
                    else
                    {
                        Character c = GetCharacter(t[1]);
                        if (c == null) break;
                        Portrait p = AddCommand<Portrait>(currBlock);
                        p._Character = c;
                        p.Display = DisplayType.Hide;
                    }
                }
                break;
            case "timehide":
                {
                    string[] t = line.Split(' ');
                    float seconds = float.Parse(t[2]);
                    LiveSpriteController m = GetModel(t[1]);
                    if (m != null)
                    {
                        InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);
                        ev.Description = line;
                        WireFloat(ev, m.HideModelTimed, seconds);
                    }
                    else 
                    {
                        Character c = GetCharacter(t[1]);
                        if (c == null) break;
                        Portrait p = AddCommand<Portrait>(currBlock);
                        p._Character = c;
                        p.Display = DisplayType.Hide;
                        p.UseDefaultSettings = false;
                        p.FadeDuration = seconds;
                    }
                }
                break;
            case "slidein":
                {
                    string[] t = line.Split(' ');
                    int expression = int.Parse(t[2]);
                    int distance = int.Parse(t[3]);
                    LiveSpriteController m = GetModel(t[1]);
                    if (m != null)
                    {
                        InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);
                        ev.Description = line;
                        WireInt(ev, m.ChangeExpression, expression);
                        WireVoid(ev, m.ShowModel);
                        WireInt(ev, m.SlideModelInX, distance);
                    }
                    else 
                    {
                        Character c = GetCharacter(t[1]);
                        if (c == null) break;
                        Portrait p = AddCommand<Portrait>(currBlock);
                        p._Character = c;
                        p.Display = DisplayType.Show;
                        p._Portrait = PortraitAt(c, expression);
                        p._Stage = stage;
                        p.Move = true;
                        p.FromPosition = StagePos(OffscreenPos(c));
                        p.ToPosition = StagePos(HomePos(c));
                    }
                }
                break;
            case "slideout":
                {
                    string[] t = line.Split(' ');
                    LiveSpriteController m = GetModel(t[1]);
                    if (m != null)
                    {
                        InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);
                        ev.Description = line;
                        WireVoid(ev, m.SlideModelOutX);
                    }
                    else
                    {
                        Character c = GetCharacter(t[1]);
                        if (c == null) break;
                        Portrait p = AddCommand<Portrait>(currBlock);
                        p._Character = c;
                        p.Display = DisplayType.Hide;
                        p._Stage = stage;
                        p.Move = true;
                        p.ToPosition = StagePos(OffscreenPos(c));
                    }
                }
                break;
            case "exp":
                {
                    string[] t = line.Split(' ');
                    int expIndex = int.Parse(t[2]);
                    LiveSpriteController m = GetModel(t[1]);
                    if (m != null)
                    {
                        InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);
                        ev.Description = line;
                        WireInt(ev, m.ChangeExpression, expIndex);
                    }
                    else
                    {
                        Character c = GetCharacter(t[1]);
                        if (c == null) break;
                        Portrait p = AddCommand<Portrait>(currBlock);
                        p._Character = c;
                        p.Display = DisplayType.Show;
                        p._Portrait = PortraitAt(c, expIndex);
                        p._Stage = stage;
                        p.ToPosition = StagePos(HomePos(c));
                    }
                }
                break;
            case "rename":
                {
                    string[] parts = line.Split(new char[] { ' ' }, 3);
                    Character c = GetCharacter(parts[1]);
                    InvokeEvent ev = AddCommand<InvokeEvent>(currBlock);
                    ev.Description = line;
                    WireString(ev, c.SetStandardText, parts[2]);
                }
                break;
            case "fade":
                {
                    FadeScreen f = AddCommand<FadeScreen>(currBlock);
                    f.Duration = float.Parse(line.Split(' ')[1]);
                    f.TargetAlpha = 1f;
                }
                break;
            case "unfade":
                {
                    FadeScreen f = AddCommand<FadeScreen>(currBlock);
                    f.Duration = float.Parse(line.Split(' ')[1]);
                    f.TargetAlpha = 0f;
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

    // map index to portrait
    Sprite PortraitAt(Character c, int index)
    {
        if (c == null || c.Portraits == null || index < 0 || index >= c.Portraits.Count) return null;
        return c.Portraits[index];
    }

    RectTransform StagePos(string posName)
    {
        return stage == null ? null : stage.GetPosition(posName);
    }

    string HomePos(Character c) => c == dukeChar ? "Left" : "Right";
    string OffscreenPos(Character c) => c == dukeChar ? "Offscreen Left" : "Offscreen Right";
#endif

    public LiveSpriteController GetModel(string modelName)
    {
        switch(modelName.ToLower())
        {
            case "m":
                return mary;
            case "h":
                return hazel;
            // case "d":    // idt it'll ever get live2d'd but commenting this out just in case
            //     return duke;
            case "a":
                return adrianne;
            default:
                // non-Live2D characters: "o", "m1", "m2", "f1", "d"
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
            case "m1":
                return m1Char;
            case "m2":
                return m2Char;
            case "f1":
                return f1Char;
            default:
                return null;
        }
    }

}
