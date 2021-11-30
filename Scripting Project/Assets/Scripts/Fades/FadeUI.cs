using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FadeUI : FadeBase
{
    private List<Graphic> graphics;
    private List<float> origAlphas;

    protected override void Initialize()
    {
        base.Initialize();

        graphics = GetComponentsInChildren<Graphic>().ToList();
        origAlphas = new List<float>();
        for(int i = 0; i < graphics.Count; i++)
        {
            origAlphas.Add(graphics[i].color.a);
        }

        _fadeUpdate = (t) =>
        {
            Color c;
            for(int i = 0; i< graphics.Count; i++)
            {
                c = graphics[i].color;
                c.a = Mathf.Lerp(0, origAlphas[i], t.CurrentValue);
                graphics[i].color = c;
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
