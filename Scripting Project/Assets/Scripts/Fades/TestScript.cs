using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TestScript : MonoBehaviour
{
    [Serializable]
    public class TestClass
    {
        public string name;
        public float num;
    }

    public List<TestClass> testList;
}
