using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DaytimeController : MonoBehaviour
{
    [SerializeField] private EnvironmentalPresets preset;
    [SerializeField] private Light DirectionalLight;

    [SerializeField, Range(0, 24)] private float TimeOfDay;

    private void Update()
    {
        if (preset == null)
            return;

        //if(Application.isPlaying){}

        UpdateLighting(TimeOfDay / 24);
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);

        if(DirectionalLight != null)
        {
            DirectionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360) - 90f, 170f, 0));
        }
    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;
        if(RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach(var light in lights)
            {
                if(light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

}
