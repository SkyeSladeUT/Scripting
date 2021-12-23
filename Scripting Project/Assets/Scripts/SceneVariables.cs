using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
public static class SceneVariables
{
    public static float MainVolume;
    public static float MusicVolume;
    public static float SFXVolume;
    public static float DialogueVolume;
    public static float DialogueSpeed;
    public static float CameraSensitivity;
    public static int CurrentScene;
    public static Vector3 CurrentTranslation;

    private static string path = "JSON/ItemsMaster";

    private static List<InventoryItem> _itemList;
    public static List<InventoryItem> ItemList
    {
        get
        {
            if(_itemList == null || _itemList.Count < 0)
            {
                string json = Resources.Load<TextAsset>(path).text;
                _itemList = JsonConvert.DeserializeObject<List<InventoryItem>>(json);
            }
            return _itemList;
        }
    }

}
