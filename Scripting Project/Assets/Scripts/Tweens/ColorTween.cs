using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class ColorTween : TweenFunctions<Color>
{
    public ColorTween()
    {
        updateFunction = (t) => { Debug.LogWarning("tween update function not set"); };
        completeFunction = (t) => { };
    }

    public ColorTween(System.Action<ITween<Color>> updateFunc, System.Action<ITween<Color>> completeFunc)
    {
        updateFunction = updateFunc;
        completeFunction = completeFunc;
    }

    public System.Action<ITween<Color>> updateFunction { get; set; }
    public System.Action<ITween<Color>> completeFunction { get; set; }

    protected override void RunCubicFunction(GameObject obj, Color InitValue, Color FinalValue, float duration, EaseFunctions easeFunction)
    {
        switch (easeFunction)
        {
            case EaseFunctions.EaseIn:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.CubicEaseIn, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseInOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.CubicEaseInOut, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.CubicEaseOut, updateFunction, completeFunction);
                break;
        }
    }

    protected override void RunLinearFunction(GameObject obj, Color InitValue, Color FinalValue, float duration)
    {
        obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.Linear, updateFunction, completeFunction);
    }

    protected override void RunQuadraticFunction(GameObject obj, Color InitValue, Color FinalValue, float duration, EaseFunctions easeFunction)
    {
        switch (easeFunction)
        {
            case EaseFunctions.EaseIn:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.QuadraticEaseIn, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseInOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.QuadraticEaseInOut, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.QuadraticEaseOut, updateFunction, completeFunction);
                break;
        }
    }

    protected override void RunQuarticFunction(GameObject obj, Color InitValue, Color FinalValue, float duration, EaseFunctions easeFunction)
    {
        switch (easeFunction)
        {
            case EaseFunctions.EaseIn:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.QuarticEaseIn, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseInOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.QuarticEaseInOut, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.QuarticEaseOut, updateFunction, completeFunction);
                break;
        }
    }

    protected override void RunQuinticFunction(GameObject obj, Color InitValue, Color FinalValue, float duration, EaseFunctions easeFunction)
    {
        switch (easeFunction)
        {
            case EaseFunctions.EaseIn:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.QuinticEaseIn, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseInOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.QuinticEaseInOut, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.QuinticEaseOut, updateFunction, completeFunction);
                break;
        }
    }

    protected override void RunSineFunction(GameObject obj, Color InitValue, Color FinalValue, float duration, EaseFunctions easeFunction)
    {
        switch (easeFunction)
        {
            case EaseFunctions.EaseIn:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.SineEaseIn, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseInOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.SineEaseInOut, updateFunction, completeFunction);
                break;
            case EaseFunctions.EaseOut:
                obj.Tween(obj.name + "Float", InitValue, FinalValue, duration, TweenScaleFunctions.SineEaseOut, updateFunction, completeFunction);
                break;
        }
    }
}
