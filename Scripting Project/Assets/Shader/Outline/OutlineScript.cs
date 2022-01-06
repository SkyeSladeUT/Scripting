using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;

    private void OnEnable()
    {
        if(transform.parent != null && transform.parent.GetComponent<OutlineScript>() != null)
        {
            return;
        }
        if(outlineRenderer == null)
        {
            outlineRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        }
        outlineRenderer.enabled = true;
    }

    Renderer CreateOutline(Material outlineMat, float scaleFactor, Color color)
    {
        GameObject outlineObject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        outlineObject.transform.localPosition = Vector3.zero;
        outlineObject.transform.localRotation = Quaternion.identity;

        Renderer rend = outlineObject.GetComponent<Renderer>();

        Destroy(outlineObject.GetComponent<OutlineScript>());
        try
        {
            Destroy(outlineObject.GetComponent<Collider>());
        }
        catch
        {

        }

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", color);
        rend.material.SetFloat("_Scale", scaleFactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;    

        rend.enabled = false;

        return rend;
    }

    private void OnDisable()
    {
        if(outlineRenderer != null)
        {
            outlineRenderer.enabled = false;
        }
    }
}
