using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class SceneTransition : MonoBehaviour
{
    // To load the scene accordingly to the scene name written during runtime only but not changing the value in the script itself
    [SerializeField] private string sceneToLoad; // Use SerializeField to have private variable shown in inspector, e.g. this while not able to be accessed by any other 
    [SerializeField] public Vector2 playerPosition; 
    [SerializeField] public GameObject fadeInPanel;
    [SerializeField] public GameObject fadeOutPanel;
    [SerializeField] public float fadeSeconds;
    
    private void Awake() 
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            SceneAnchor.transitionPosition = playerPosition; // To set the value of _transitionPosition to playerPosition
            StartCoroutine(FadeCo()); 
        }
    }

    public IEnumerator FadeCo() 
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeSeconds);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
