using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFuctions : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject SoundMenu;
    [SerializeField] private GameObject ControlsMenu;

    public void Continue()
    {
        IterationManager.Instance.LoadLevel();
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
    }

    public void Sound()
    {
        SoundMenu.SetActive(true);
    }

    public void Controls()
    {
        ControlsMenu.SetActive(true);
    }

    public void Credits()
    {
        SceneManager.LoadScene("L13_Loop1");
    }

    public void BackToDesk()
    {
        IterationManager.Instance.ExitGame();
    }
}
