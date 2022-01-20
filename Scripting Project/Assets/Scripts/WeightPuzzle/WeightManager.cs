using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightManager : MonoBehaviour
{
    public List<WeightObject> objects;
    private int _side01, _side02;
    public float MaxDifference;
    public Animator ScaleAnim;

    public int DesiredWeight;

    public void AddWeight(bool side01, int weight)
    {
        if (side01)
            _side01 += weight;
        else
            _side02 += weight;
        UpdateScale();
    }

    public void RemoveWeight(bool side01, int weight)
    {
        if (side01)
            _side01 -= weight;
        else
            _side02 -= weight;
        UpdateScale();
    }

    private void UpdateScale()
    {
        float Difference = _side01 - _side02;
        Difference = Mathf.Clamp(Difference, -MaxDifference, MaxDifference);
        float DifferenceNormalized = Difference / MaxDifference;
        ScaleAnim.SetFloat("ScaleDifference", DifferenceNormalized);
    }

    public void CheckWeight()
    {

    }
}
