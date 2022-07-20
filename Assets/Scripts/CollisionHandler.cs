using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's ok buddy!");
                break;
            case "Finish":
                // Debug.Log("Congratulations, you finished!");
                LoadNextLevel();
                break;
            case "Fuel":
                Debug.Log("Your tank is full!");
                break;
            default:
                // Debug.Log("Ups!!");
                ReloadLevel();
                break;
        }
    }
    
    // The game will restart when you hit the obstacle.
    void ReloadLevel()
    {
        // SceneManager.LoadScene("Level1");
        // SceneManager.LoadScene(0); // index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    // When we reach the LandingPad, load to next level
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // current level, which is zero
        int nextSceneIndex = currentSceneIndex + 1;
        
        // how many scenes do we have
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
