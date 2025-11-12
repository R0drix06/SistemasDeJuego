using UnityEngine;

public class PauseFuctions : MonoBehaviour, IUpdatable
{
    private bool isPause = false;
    [SerializeField] private GameObject pausePanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CustomUpdateManager.Instance.Register(this);
        IterationManager.Instance.updatables.Add(this);
    }

    public void Tick(float deltaTime)
    {
        if (Input.GetKeyUp(KeyCode.Escape) && !isPause)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPause = true;
            Debug.Log("Enter");

        }
        else if (Input.GetKeyUp(KeyCode.Escape) && isPause)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
            isPause = false;
            Debug.Log("Exit");
        }
    }
    
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        IterationManager.Instance.Unregister();
        IterationManager.Instance.LoadMenu();
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        isPause = false;
    }
}
