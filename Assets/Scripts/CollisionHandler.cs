using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor.
    [SerializeField] float delay = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip winSound;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem winParticles;

    // CACHE - e.g. references for readibility or speed.
    AudioSource audioSource;

    // STATE - private instance (member) variable
    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    // for developer
    void RespondToDebugKeys()
    {
        // GetKeyDown will happen once when you hit the key
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) // isTransitioning || collisionDisabled == true
        {
            return; 
        }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's ok buddy!");
                break;
            case "Finish":
                // Debug.Log("Congratulations, you finished!");
                StartSuccessSequence();
                break;
            default:
                // Debug.Log("Ups!!");
                StartCrashSequence();
                break;
        }
    }

     void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(winSound);
        // Untick play on awake
        winParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

     void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        // Todo add SFX upon crash
        audioSource.PlayOneShot(crashSound);
        // We want to remove the control from player so the player can not be flying after they crash.
        // So when we crushed, unchecked tickbox (movement script).
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        // Get ReloadLevel 1 second after crushing.
        Invoke("ReloadLevel", delay);
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
