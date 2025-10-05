using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IterationManager : MonoBehaviour
{
    public static IterationManager Instance;
    public int level = 1;

    [SerializeField] private int MaxLevel;

    public List<IUpdatable> updatables = new List<IUpdatable>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public string CurrentLevel()
    {
        return "Level" + level.ToString();
    }

    public void ResetLevel()
    {
        Unregister();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Unregister();

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

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Unregister()
    {
        
        foreach (var item in updatables)
        {
            CustomUpdateManager.Instance.Unregister(item);
        }

        updatables.Clear();
        
    }
}
