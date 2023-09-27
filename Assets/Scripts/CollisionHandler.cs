using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip winSFX;
    [SerializeField] float reloadDelay = 1f;

    AudioSource sfx;

    public bool isTransitioning = false;
    void Start()
    {
        sfx = GetComponent<AudioSource>(); 
    }
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag) {
            case "Finish":
                LoadNextLevel();
                break;
            case "Friendly":
                Debug.Log("you are back at start ");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        if(isTransitioning == false) 
        {
            isTransitioning = true;
            sfx.Stop();
            sfx.PlayOneShot(crashSFX);
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", reloadDelay);
        }
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        if(isTransitioning == false)
        {
            isTransitioning = true;
            sfx.Stop();
            sfx.PlayOneShot(winSFX);
            GetComponent<Movement>().enabled = false;
            Invoke("LoadLevel", reloadDelay);
        }
    }
    void LoadLevel()
    {
         int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
         int nextSceneIndex = currentSceneIndex + 1;

         if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
         {
            nextSceneIndex = 0;
         }
         SceneManager.LoadScene(nextSceneIndex);
    }
}
