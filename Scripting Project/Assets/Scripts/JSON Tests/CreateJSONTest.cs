using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
public class CreateJSONTest : MonoBehaviour
{
    [Serializable]
    public class TestClass
    {
        public string name;
        public int num;
        [NonSerialized]
        public Sprite sprite;        
    }

    public List<TestClass> list;
    [TextArea(5, 50)]
    public string json;
    private void Start()
    {
        json = JsonConvert.SerializeObject(list);
    }

}
