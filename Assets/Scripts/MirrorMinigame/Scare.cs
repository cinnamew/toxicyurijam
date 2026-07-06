using UnityEngine;
using UnityEngine.SceneManagement;

public class Scare : MonoBehaviour
{
    public void OnScareFinished()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("im actually quaking in my boots no for real im probably the biggest scaredy cat here");
    }
}

