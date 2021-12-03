using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class TileObject
{
    private GameObject rotateTemp;
    private Quaternion initRotation, rightRotation, leftRotation;

    private GameObject _gameObject;
    public GameObject gameObject
    {
        get { return _gameObject; }
    }

    private int _num;
    public int Num
    {
        get { return _num; }
        set { _num = value; }
    }

    public TileObject (GameObject obj)
    {
        _gameObject = obj;
    }

    public void Wiggle()
    {
        if (rotateTemp == null)
        {
            rotateTemp = new GameObject();
            rotateTemp.transform.rotation = gameObject.transform.rotation;
            initRotation = rotateTemp.transform.rotation;
            rotateTemp.transform.Rotate(new Vector3(0, 20, 0), Space.Self);
            rightRotation = rotateTemp.transform.rotation;
            rotateTemp.transform.Rotate(new Vector3(0, -40, 0), Space.Self);
            leftRotation = rotateTemp.transform.rotation;
            rotateTemp.SetActive(false);
        }

        gameObject.Tween(gameObject.name + "Wiggle", initRotation, rightRotation, .05f, TweenScaleFunctions.CubicEaseIn, (t) => { gameObject.transform.rotation = t.CurrentValue; })
            .ContinueWith(new QuaternionTween().Setup(rightRotation, leftRotation, .1f, TweenScaleFunctions.Linear, (t) => { gameObject.transform.rotation = t.CurrentValue; }))
            .ContinueWith(new QuaternionTween().Setup(leftRotation, rightRotation, .1f, TweenScaleFunctions.Linear, (t) => { gameObject.transform.rotation = t.CurrentValue; }))
            .ContinueWith(new QuaternionTween().Setup(rightRotation, initRotation, .05f, TweenScaleFunctions.CubicEaseOut, (t) => { gameObject.transform.rotation = t.CurrentValue; }));
    }

}
