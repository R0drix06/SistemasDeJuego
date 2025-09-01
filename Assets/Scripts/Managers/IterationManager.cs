using UnityEngine;
using UnityEngine.SceneManagement;

public class IterationManager : MonoBehaviour
{
    public static IterationManager Instance;

    private void Awake()
    {
        if (IterationManager.Instance == null)
        {
            IterationManager.Instance = new IterationManager();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
