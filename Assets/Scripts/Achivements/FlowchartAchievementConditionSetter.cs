using System;
using Fungus;
using UnityEngine;

[RequireComponent(typeof(Flowchart))]
public class FlowchartAchievementConditionSetter : MonoBehaviour
{
    private Flowchart _flowchart;
    private void Start()
    {
        _flowchart = GetComponent<Flowchart>();
    }

    private void Update()
    {
        _flowchart.SetIntegerVariable("endings",AchivementManager.Instance.EndingsCompleted());
    }
}
