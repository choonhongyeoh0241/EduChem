using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader {
    private const string preloadSceneName = "_Preload";

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Preload() {
        // SceneManager.sceneLoaded += UnloadSelf;
        SceneManager.LoadScene(preloadSceneName, LoadSceneMode.Additive);
    }

    // private static void UnloadSelf(Scene scene, LoadSceneMode mode) {
    //     Debug.Log(scene.name);
    //     if (scene.name != preloadSceneName) {
    //         SceneManager.UnloadSceneAsync(preloadSceneName);
    //         SceneManager.sceneLoaded -= UnloadSelf;
    //     }
    // }

    //! Sections commented out don't work as intended — `UnloadSelf` doesn't get called for the preload scene?
}
