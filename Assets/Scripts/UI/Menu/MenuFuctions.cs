using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuFuctions : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject SoundMenu;
    [SerializeField] private GameObject ControlsMenu;
    [SerializeField] private GameObject GlobalVolume;

    public static bool VolumeIsOn = true;

    public void ActivateVolume()
    {
        if (GlobalVolume.activeInHierarchy)
        {
            GlobalVolume.SetActive(false);
        }
        else
        {
            GlobalVolume.SetActive(true);
        }
    }

    public void Continue()
    {
        IterationManager.Instance.currentLevel = 1;
        IterationManager.Instance.currentLoop = 1;
        SceneManager.LoadScene("L1_Loop1");
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
        SceneManager.LoadScene("L16_Loop1");
    }

    public void BackToDesk()
    {
        IterationManager.Instance.ExitGame();
    }
}
