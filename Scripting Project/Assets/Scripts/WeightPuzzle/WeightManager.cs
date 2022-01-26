using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightManager : MonoBehaviour
{
    public List<WeightGrab> weights;
    private int _side01, _side02;
    public float MaxDifference;
    public Animator ScaleAnim;

    public int DesiredWeight;
    public WeightSide Side01, Side02;

    public void Initialize()
    {
        foreach(var w in weights)
        {
            w.onEndDrag = context =>
            {
                PlaceWeight(w);
            };
        }
    }

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
        ScaleAnim.SetFloat("Tilt", DifferenceNormalized);
    }

    public void CheckWeight()
    {

    }

    private void PlaceWeight(WeightGrab w)
    {
        RaycastHit[] hits = Physics.RaycastAll(w.transform.position, -Camera.main.transform.up, 10);
        Debug.Log("Hits: " + hits.Length);
        for(int i = 0; i < hits.Length; i++)
        {
            WeightSide temp;
            if((temp = hits[i].collider.GetComponent<WeightSide>()) != null)
            {
                Debug.Log("Hit: " + hits[i].collider.name);
                AddWeight(temp.RightSide, w.weight);
                return;
            }
        }
        w.ResetPosition();
    }
}
