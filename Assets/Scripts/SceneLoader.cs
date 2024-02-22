using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class SceneLoader : MonoSingleton<SceneLoader>
{
    public void ReloadCurrentScene(float delay = 0)
    {
        StartCoroutine(DelayedReload(delay));
    }

    private IEnumerator DelayedReload(float delay)
    {
        yield return new WaitForSeconds(delay);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}