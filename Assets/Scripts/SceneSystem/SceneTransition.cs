using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; 
    [SerializeField] public Vector2 playerPosition; 
    [SerializeField] public GameObject loadingScreen;
    [SerializeField] private Image progressBar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            SceneAnchor.transitionPosition = playerPosition; 
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene() 
    {
        progressBar.fillAmount = 0;
        loadingScreen.SetActive(true);
        
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;
        float progress = 0;

        while (!asyncOperation.isDone)
        {
            progress = Mathf.MoveTowards(progress, asyncOperation.progress, 0.5f * Time.deltaTime);
            progressBar.fillAmount = progress;

            if(progress >= 0.9f)
            {
                progressBar.fillAmount = 1;
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
