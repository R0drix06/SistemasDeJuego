using UnityEngine;

public class FullscreenMaterialController : MonoBehaviour
{
    private IterationManager iterationManager;

    [SerializeField] private Material fullscreenMaterial;

    private static readonly int EnabledID =
        Shader.PropertyToID("_Scanlines_Enabled");

    void Awake()
    {

        GameObject itObject = GameObject.FindWithTag("IT");

        if (itObject == null)
        {
            Debug.LogError("No se encontró ningún GameObject con tag IT");
            return;
        }

        iterationManager = itObject.GetComponent<IterationManager>();

        if (iterationManager == null)
            Debug.LogError("El GameObject con tag IT no tiene IterationManager");
    }

    public void EnableEffect()
    {
        fullscreenMaterial.SetFloat(EnabledID, 1f);
        iterationManager.isCRTon = true;
    }

    public void DisableEffect()
    {
        fullscreenMaterial.SetFloat(EnabledID, 0f);
        iterationManager.isCRTon = false;
    }

    public void SetEffect(bool enabled)
    {
        fullscreenMaterial.SetFloat(EnabledID, enabled ? 1f : 0f);
    }

}
