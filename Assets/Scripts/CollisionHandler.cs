using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
            {
                Debug.Log("Collided With Friendly Object");
                break;
            }
            case "Finish":
            {
                LoadNextScene();
                break;
            }
            case "Fuel":
            {
                Debug.Log("Collided With Fuel Object");
                break;
            }
            default:
            {
                // When rocket hits obstacles (anything that isn't specified above) we reload the current level
                ReloadCurrentScene();
                break;
            }
        }
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
    private void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
