using UnityEngine;
using UnityEngine.SceneManagement;

public class IterationManager : MonoBehaviour
{
    public static IterationManager Instance;
    public int level = 1;

    [SerializeField] private int MaxLevel;

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

    public string CurrentLevel()
    {
        return "Level" + level.ToString();
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        if (level >= MaxLevel)
        {
            level = 1;
        }
        else
        {
            level++;
        }

        SceneManager.LoadScene("Level" + level.ToString());
    }
}
