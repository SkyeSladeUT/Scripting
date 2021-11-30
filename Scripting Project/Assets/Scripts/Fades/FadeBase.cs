using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DigitalRuby.Tween;
using System;
using System.Linq;

public abstract class FadeBase : MonoBehaviour
{
    public enum eAnimationType
    {
        CubicEaseIn, CubicEaseInOut, CubicEaseOut,
        Linear,
        QuadraticEaseIn, QuadraticEaseInOut, QuadraticEaseOut,
        QuarticEaseIn, QuarticEaseInOut, QuarticEaseOut,
        QuinticEaseIn, QuinticEaseInOut, QuinticEaseOut,
        SineEaseIn, SineEaseInOut, SineEaseOut
    }

    public float InitWaitTime;
    public float FadeTime;
    public UnityEvent OnFadeIn, OnFadeOut;
    protected System.Action<ITween<float>> _fadeUpdate, _fadeInComplete, _fadeOutComplete;
    protected Func<float, float> _tweenScale;
    public eAnimationType AnimationType;

    private void Awake()
    {
        Initialize();
    }

    protected virtual void Initialize()
    {
        switch (AnimationType)
        {
            case eAnimationType.CubicEaseIn:
                _tweenScale = TweenScaleFunctions.CubicEaseIn;
                break;
            case eAnimationType.CubicEaseInOut:
                _tweenScale = TweenScaleFunctions.CubicEaseInOut;
                break;
            case eAnimationType.CubicEaseOut:
                _tweenScale = TweenScaleFunctions.CubicEaseOut;
                break;
            case eAnimationType.Linear:
                _tweenScale = TweenScaleFunctions.Linear;
                break;
            case eAnimationType.QuadraticEaseIn:
                _tweenScale = TweenScaleFunctions.QuadraticEaseIn;
                break;
            case eAnimationType.QuadraticEaseInOut:
                _tweenScale = TweenScaleFunctions.QuadraticEaseInOut;
                break;
            case eAnimationType.QuadraticEaseOut:
                _tweenScale = TweenScaleFunctions.QuadraticEaseOut;
                break;
            case eAnimationType.QuarticEaseIn:
                _tweenScale = TweenScaleFunctions.QuarticEaseIn;
                break;
            case eAnimationType.QuarticEaseInOut:
                _tweenScale = TweenScaleFunctions.QuarticEaseInOut;
                break;
            case eAnimationType.QuarticEaseOut:
                _tweenScale = TweenScaleFunctions.QuarticEaseOut;
                break;
            case eAnimationType.QuinticEaseIn:
                _tweenScale = TweenScaleFunctions.QuinticEaseIn;
                break;
            case eAnimationType.QuinticEaseInOut:
                _tweenScale = TweenScaleFunctions.QuinticEaseInOut;
                break;
            case eAnimationType.QuinticEaseOut:
                _tweenScale = TweenScaleFunctions.QuinticEaseOut;
                break;
            case eAnimationType.SineEaseIn:
                _tweenScale = TweenScaleFunctions.SineEaseIn;
                break;
            case eAnimationType.SineEaseInOut:
                _tweenScale = TweenScaleFunctions.SineEaseInOut;
                break;
            case eAnimationType.SineEaseOut:
                _tweenScale = TweenScaleFunctions.SineEaseOut;
                break;
        }
    }

    public void FadeIn()
    {
        StartCoroutine(InitWait(true));
    }

    public void FadeOut()
    {
        StartCoroutine(InitWait(false));
    }

    private IEnumerator InitWait(bool fadein)
    {
        yield return new WaitForSeconds(FadeTime);
        FadeFunction(fadein);
    }

    protected virtual void FadeFunction(bool fadein)
    {
        if (fadein)
        {
            gameObject.Tween(this.GetType().Name + "FI" + gameObject.name, 0, 1, FadeTime, _tweenScale, _fadeUpdate, _fadeInComplete);
        }
        else
        {
            gameObject.Tween(this.GetType().Name + "FO" + gameObject.name, 1, 0, FadeTime, _tweenScale, _fadeUpdate, _fadeInComplete);
        }
    }


}
