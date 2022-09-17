using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader {
    private const string preloadSceneName = "_Preload";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Preload() {
        SceneManager.LoadScene(preloadSceneName, LoadSceneMode.Additive);
    }
}
