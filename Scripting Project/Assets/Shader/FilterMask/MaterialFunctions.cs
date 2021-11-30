using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialFunctions : MonoBehaviour
{
    public List<Texture2D> LightMaps;
    public Material mat;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeLighting(-1);
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeLighting(0);
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeLighting(1);
        }
    }

    public void ChangeLighting(int lightSetting)
    {
        int ls = Mathf.Clamp(lightSetting, -1, LightMaps.Count - 1);
        if(ls == -1)
        {
            mat.SetTexture("_LightingMask", null);
        }
        else
        {
            mat.SetTexture("_LightingMask", LightMaps[ls]);
        }
    }

}
