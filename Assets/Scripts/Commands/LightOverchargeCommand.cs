using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightOverchargeCommand
{
    private Light2D light;
    private Color overchargeColor;
    private Color baseColor;

    public LightOverchargeCommand (Light2D light)
    {
        this.light = light;
        ColorUtility.TryParseHtmlString("#72EDFF", out Color color1);
        baseColor = color1;
        ColorUtility.TryParseHtmlString("#FF62D0", out Color color2);
        overchargeColor = color2;
    }

    public void Execute(bool activate)
    {
        if (activate)
        {
            light.intensity = 5f;
            light.color = overchargeColor;
        }
        if (!activate)
        {
            light.intensity = 2.5f;
            light.color = baseColor;
        }
    }
}
