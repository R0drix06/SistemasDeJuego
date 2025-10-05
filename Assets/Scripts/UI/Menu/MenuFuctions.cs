using UnityEngine;

public class MenuFuctions : MonoBehaviour
{
    public void Continue()
    {
        IterationManager.Instance.LoadLevel();
    }

    public void BackToDesk()
    {
        IterationManager.Instance.ExitGame();
    }
}
