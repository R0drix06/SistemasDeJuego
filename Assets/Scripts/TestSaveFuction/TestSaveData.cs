using UnityEngine;

public class TestSaveData : MonoBehaviour
{
  

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            GameManager.Instance.SaveProgress();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.LoadSave();
        }
    }
}
