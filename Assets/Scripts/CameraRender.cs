using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraRender : MonoBehaviour
{
    private UniversalAdditionalCameraData cameraData;

    public void SetPostProcessing(bool enabled)
    {
        cameraData.renderPostProcessing = enabled;
    }

    private IterationManager iterationManager;

    void Awake()
    {
        Camera cam = GetComponent<Camera>();
        cameraData = cam.GetUniversalAdditionalCameraData();

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


    // Update is called once per frame
    void Update()
    {
        if (iterationManager.isCRTon)
        {
            SetPostProcessing(true);
        }
        else
        {
            SetPostProcessing(false);
        }
    }
}
