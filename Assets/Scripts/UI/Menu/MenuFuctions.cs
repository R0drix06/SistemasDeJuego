using UnityEngine;

public class MenuFuctions : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject SoundMenu;

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

    public void BackToDesk()
    {
        IterationManager.Instance.ExitGame();
    }
}
