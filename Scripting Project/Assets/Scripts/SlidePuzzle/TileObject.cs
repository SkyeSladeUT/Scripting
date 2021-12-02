using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject
{
    private GameObject rotateTemp;
    private QuaternionTween RotateTween01, RotateTween02, RotateTween03, RotateTween04;
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
            RotateTween01 = new QuaternionTween((t) => { gameObject.transform.rotation = t.CurrentValue; }, (t) =>
            {
                RotateTween02.RunTween(gameObject, gameObject.transform.rotation, leftRotation, .1f, ScaleFunctions.Linear, EaseFunctions.EaseInOut);
            });
            RotateTween02 = new QuaternionTween((t) => { gameObject.transform.rotation = t.CurrentValue; }, (t) =>
           {
               RotateTween03.RunTween(gameObject, gameObject.transform.rotation, rightRotation, .1f, ScaleFunctions.Linear, EaseFunctions.EaseInOut);
           });
            RotateTween03 = new QuaternionTween((t) => { gameObject.transform.rotation = t.CurrentValue; }, (t) =>
             {
                 RotateTween04.RunTween(gameObject, gameObject.transform.rotation, initRotation, .05f, ScaleFunctions.Cubic, EaseFunctions.EaseOut);
             });
            RotateTween04 = new QuaternionTween((t) => { gameObject.transform.rotation = t.CurrentValue; }, (t) => {});
        }
        RotateTween01.RunTween(gameObject, gameObject.transform.rotation, rightRotation, .05f, ScaleFunctions.Cubic, EaseFunctions.EaseIn);

    }

}
