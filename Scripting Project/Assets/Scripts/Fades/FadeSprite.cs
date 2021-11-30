using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FadeSprite : FadeBase
{
    private List<SpriteRenderer> sprites;
    private List<float> origAlphas;

    protected override void Initialize()
    {
        base.Initialize();

        sprites = GetComponentsInChildren<SpriteRenderer>().ToList();
        origAlphas = new List<float>();
        for (int i = 0; i < sprites.Count; i++)
        {
            origAlphas.Add(sprites[i].color.a);
        }

        _fadeUpdate = (t) =>
        {
            Color c;
            for (int i = 0; i < sprites.Count; i++)
            {
                c = sprites[i].color;
                c.a = Mathf.Lerp(0, origAlphas[i], t.CurrentValue);
                sprites[i].color = c;
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
