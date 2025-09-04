using UnityEngine;
using UnityEngine.SceneManagement;

public class IterationManager : MonoBehaviour
{
    public static IterationManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void ResetLevel()
    {
        //Scene actualScene = SceneManager.GetActiveScene();
        //string sceneName = actualScene.name;
        //SceneManager.LoadScene(sceneName);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
