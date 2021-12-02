using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public enum ScaleFunctions
{
    Cubic, Linear, Quartic, Quintic, Quadratic, Sine
}
public enum EaseFunctions
{
    EaseIn, EaseOut, EaseInOut
}

public abstract class TweenFunctions<T>
{
    public void RunTween(GameObject obj, T InitValue, T FinalValue, float duration, ScaleFunctions scaleFunction, EaseFunctions easeFunction)
    {
        switch (scaleFunction)
        {
            case ScaleFunctions.Cubic:
                RunCubicFunction(obj, InitValue, FinalValue, duration, easeFunction);
                break;
            case ScaleFunctions.Linear:
                RunLinearFunction(obj, InitValue, FinalValue, duration);
                break;
            case ScaleFunctions.Quadratic:
                RunQuadraticFunction(obj, InitValue, FinalValue, duration, easeFunction);
                break;
            case ScaleFunctions.Quartic:
                RunQuarticFunction(obj, InitValue, FinalValue, duration, easeFunction);
                break;
            case ScaleFunctions.Quintic:
                RunQuinticFunction(obj, InitValue, FinalValue, duration, easeFunction);
                break;
            case ScaleFunctions.Sine:
                RunSineFunction(obj, InitValue, FinalValue, duration, easeFunction);
                break;
        }
    }
    protected abstract void RunCubicFunction(GameObject obj, T InitValue, T FinalValue, float duration, EaseFunctions easeFunction);
    protected abstract void RunLinearFunction(GameObject obj, T InitValue, T FinalValue, float duration);
    protected abstract void RunQuadraticFunction(GameObject obj, T InitValue, T FinalValue, float duration, EaseFunctions easeFunction);
    protected abstract void RunQuarticFunction(GameObject obj, T InitValue, T FinalValue, float duration, EaseFunctions easeFunction);
    protected abstract void RunQuinticFunction(GameObject obj, T InitValue, T FinalValue, float duration, EaseFunctions easeFunction);
    protected abstract void RunSineFunction(GameObject obj, T InitValue, T FinalValue, float duration, EaseFunctions easeFunction);




}
