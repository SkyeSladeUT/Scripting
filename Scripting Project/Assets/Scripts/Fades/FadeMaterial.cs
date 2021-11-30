using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using System.Linq;

public class FadeMaterial : FadeBase
{
    public float MaxOpacity, MinOpacity;
    public string OpacityString = "";
    private List<Material> mats;
    private List<MeshRenderer> meshRends;
    private List<SkinnedMeshRenderer> skinRends;
    private List<float> origColorAlphas, origStringAlphas;

    protected override void Initialize()
    {
        base.Initialize();
        meshRends = GetComponentsInChildren<MeshRenderer>().ToList();
        skinRends = GetComponentsInChildren<SkinnedMeshRenderer>().ToList();
        mats = new List<Material>();
        origColorAlphas = new List<float>();
        origStringAlphas = new List<float>();

        foreach (var r in meshRends)
        {
            foreach (var m in r.materials)
            {
                mats.Add(m);
                origColorAlphas.Add(m.color.a);
                if (OpacityString != "")
                    origStringAlphas.Add(m.GetFloat(OpacityString));
            }
        }
        foreach (var r in skinRends)
        {
            foreach (var m in r.materials)
            {
                mats.Add(m);
                origColorAlphas.Add(m.color.a);
                if (OpacityString != "")
                    origStringAlphas.Add(m.GetFloat(OpacityString));
            }
        }

        _fadeUpdate = (t) =>
        {
            Color c;
            for (int i = 0; i < mats.Count; i++)
            {
                if (mats[i] != null)
                {
                    c = mats[i].color;
                    c.a = Mathf.Lerp(0, origColorAlphas[i], t.CurrentValue);
                    mats[i].color = c;
                    if (OpacityString != "")
                        mats[i].SetFloat(OpacityString, Mathf.Lerp(0, origStringAlphas[i], t.CurrentValue));
                }
            }
        };
        _fadeInComplete = (t) =>
        {
            OnFadeIn.Invoke();
        };
        _fadeOutComplete = (t) =>
        {
            OnFadeOut.Invoke();
        };
    }
}
