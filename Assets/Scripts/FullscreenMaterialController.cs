using UnityEngine;

public class FullscreenMaterialController : MonoBehaviour
{
    [SerializeField] private Material fullscreenMaterial;

    private static readonly int EnabledID =
        Shader.PropertyToID("_Scanlines_Enabled");

    public void EnableEffect()
    {
        fullscreenMaterial.SetFloat(EnabledID, 1f);
    }

    public void DisableEffect()
    {
        fullscreenMaterial.SetFloat(EnabledID, 0f);
    }

    public void SetEffect(bool enabled)
    {
        fullscreenMaterial.SetFloat(EnabledID, enabled ? 1f : 0f);
    }

}
