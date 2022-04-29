using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(menuName="Environment/EnvironmentalPresets")]
public class EnvironmentalPresets : ScriptableObject
{
    public Gradient FogColor;
    public Gradient DirectionalColor;
    public Gradient AmbientColor;
}
