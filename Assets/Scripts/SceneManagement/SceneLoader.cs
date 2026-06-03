using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : PersistentSingleton<SceneLoader>
{
    [SerializeField] private CanvasGroup fadeTransition;
    private const float fadeOutDurationInSeconds = 1f;
    private const float fadeInDurationInSeconds = 0.5f;


    private IEnumerator LoadSceneTransition(string sceneName)
    {
        fadeTransition.blocksRaycasts = true;
        yield return fadeTransition.DOFade(1, fadeInDurationInSeconds).WaitForCompletion();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone) yield return null;
        
        yield return fadeTransition.DOFade(0, fadeOutDurationInSeconds).WaitForCompletion();
        fadeTransition.blocksRaycasts = false;
    }

    public void LoadNextScene() => LoadSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);

    public void LoadSceneByBuildIndex(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        StartCoroutine(nameof(LoadSceneTransition), sceneName);
    }

    // public void LoadSaveSlot(string slotName) => StartCoroutine(nameof(LoadSceneTransition), SaveLoadSystem.Instance.GetSlotLevelName(slotName));
}
