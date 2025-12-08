using UnityEngine;

public class VFX_Manager : MonoBehaviour
{
    public static VFX_Manager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
