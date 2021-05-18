using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private AudioSource _audioSource;
    private Movement _movement;

    private bool _isTransitioning = false;
    private bool _isCollisionEnable = true;
    
    [SerializeField] private float _delayOnCrash = 1f;
    [SerializeField] private float _delayOnFinish = 1f;
    [SerializeField] private AudioClip _deathAudioClip;
    [SerializeField] private AudioClip _successAudioClip;
    [SerializeField] private ParticleSystem _successParticleSystem;
    [SerializeField] private ParticleSystem _deathParticleSystem;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        // If we are currently transition dont process any other collisions
        // Or we have toggle collision off should only be used for testing and debugging
        if (_isTransitioning || !_isCollisionEnable) { return; }

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
        _isTransitioning = true;
        
        // Play Death Sound Effect
        _audioSource.Stop();
        _audioSource.PlayOneShot(_deathAudioClip);
        
        // Play Particle effect on crash
        _deathParticleSystem.Play();

        // Disable movement component when we crash
        _movement.enabled = false;
        
        // Reload the Scene
        // Added a delay so less jarring when dying
        Invoke("ReloadCurrentScene", delayAmount);
    }

    private void StartFinishSequence(float delayAmount)
    {
        // Set isTransitioning to false so that when we succeed we dont do anything else
        _isTransitioning = true;

        // Play Success Sound Effect
        _audioSource.Stop();
        _audioSource.PlayOneShot(_successAudioClip);
        
        // Play Particle effect on success
        _successParticleSystem.Play();
        
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

    private void RespondToDebugKeys()
    {
        // Load the next level when the user presses L should only be used for testing and debugging
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        // Toggle collision of the rocket should only be used for testing and debugging
        else if (Input.GetKeyDown(KeyCode.C))
        {
            _isCollisionEnable = !_isCollisionEnable; // Toggle collision on and off
        }
    }
}
