using System;
using Fungus;
using UnityEngine;

[RequireComponent(typeof(Flowchart))]
public class FlowchartAchievementConditionSetter : MonoBehaviour
{
    private Flowchart _flowchart;
    private AchivementManager _am;
    private void Start()
    {
        _flowchart = GetComponent<Flowchart>();
        _am = AchivementManager.Instance;
    }

    private void Update()
    {
        var complete = _am.GetAchievement(Achievement.Ending1) &&
                       _am.GetAchievement(Achievement.Ending2) &&
                       _am.GetAchievement(Achievement.Ending3);
        
        _flowchart.SetBooleanVariable("completed_all_endings",complete);
    }
}
