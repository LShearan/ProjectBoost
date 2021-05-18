using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private AudioSource _audioSource;
    private Movement _movement;

    private bool isTransitioning = false;
    
    [SerializeField] private float _delayOnCrash = 1f;
    [SerializeField] private float _delayOnFinish = 1f;
    [SerializeField] private AudioClip _deathAudioClip;
    [SerializeField] private AudioClip _successAudioClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _movement = GetComponent<Movement>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        // If we are currently transition dont process any other collisions
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
            {
                break;
            }
            case "Finish":
            {
                StartFinishSequence(_delayOnFinish);
                break;
            }
            default:
            {
                // When rocket hits an obstacles (anything that isn't specified above) we reload the current level
                StartCrashSequence(_delayOnCrash);
                break;
            }
        }
    }

    private void StartCrashSequence(float delayAmount)
    {
        // Set isTransitioning to false so that when we crash we dont do anything else
        isTransitioning = true;
        
        // Play Death Sound Effect
        _audioSource.Stop();
        _audioSource.PlayOneShot(_deathAudioClip);
        
        // todo add particle effect on crash

        // Disable movement component when we crash
        _movement.enabled = false;
        
        // Reload the Scene
        // Added a delay so less jarring when dying
        Invoke("ReloadCurrentScene", delayAmount);
    }

    private void StartFinishSequence(float delayAmount)
    {
        // Set isTransitioning to false so that when we succeed we dont do anything else
        isTransitioning = true;

        // Play Success Sound Effect
        _audioSource.Stop();
        _audioSource.PlayOneShot(_successAudioClip);
        
        // Disable movement component when we reach the end of the level
        _movement.enabled = false;
        
        // Load the next level with a delay
        Invoke("LoadNextScene", delayAmount);
    }
    
    private void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        // If the next is the last scene in the build settings then we load the very first scene
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;

        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
