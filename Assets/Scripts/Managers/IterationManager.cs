using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IterationManager : MonoBehaviour
{
    public static IterationManager Instance;
    public int currentLevel = 1;
    public int currentLoop = 1;

    private int MaxLevel = 6;

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
        return "Level" + currentLevel.ToString();
    }

    public void ResetLevel()
    {
        Unregister();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel()
    {
        Unregister();

        if (currentLevel > MaxLevel)
        {
            currentLevel = 1;
            currentLoop = 1;
        }
        else
        {
            currentLevel++;
            currentLoop = 1;
        }

        SceneManager.LoadScene("L" + currentLevel.ToString() + "_Loop" + currentLoop.ToString());
    }

    public void NextLoop()
    {
        Unregister();

        currentLoop ++;

        SceneManager.LoadScene("L" + currentLevel.ToString() + "_Loop" + currentLoop.ToString());
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("L1_Loop1");
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
