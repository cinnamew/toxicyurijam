using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class AchivementManager : Singleton<AchivementManager>
{
    [Button("RESET ACHIEVEMENTS")]
    public void ResetAchievements()
    {
        foreach (var achievement in Enum.GetValues(typeof(Achievement)))
            PlayerPrefs.DeleteKey(achievement.ToString());
    }
    
    [Button("PRINT ACHIEVEMENTS")]
    public void PrintAchievements()
    {
        foreach (var achievement in Enum.GetValues(typeof(Achievement)))
        {
            if(PlayerPrefs.HasKey(achievement.ToString()))
                Debug.Log(achievement.ToString()+ " : " +PlayerPrefs.GetInt(achievement.ToString()));
        }
    }
    public void SetEndingAchievement(int ending) => PlayerPrefs.SetInt("Ending" + ending.ToString(), 1);
    
    public bool GetAchievement(Achievement achievement) => PlayerPrefs.GetInt(achievement.ToString(), 0) == 1;
}
  public enum Achievement
    {
        Ending1,
        Ending2,
        Ending3
    }