using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.U2D;
using Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace GameManagement {
    public class GameDataManager : MonoBehaviour
    {
        private string FilePath;
        private GameDataManager _instance;
        public GameDataManager Instance
        {
            get { return _instance; }
        }

        private GameData _data;
        public GameData Data
        {
            get { return _data; }
        }

        public GameData initData;

        private string InitDataPath = "JSON/InitData.json";

        private void Awake()
        {
            FilePath = Application.persistentDataPath + "/GameData.json";
            if (File.Exists(FilePath))
            {
                string jsonStr = File.ReadAllText(FilePath);
                _data = JsonConvert.DeserializeObject<GameData>(jsonStr);
            }
            else
            {
                string jsonStr = Resources.Load<TextAsset>(InitDataPath).text;
                _data = JsonConvert.DeserializeObject<GameData>(jsonStr);
                SaveGame();
            }

            /*string jsonstr = JsonConvert.SerializeObject(initData);
            Debug.Log(FilePath);
            File.WriteAllText(FilePath, jsonstr);*/

            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            DontDestroyOnLoad(this.gameObject);
            _instance = this;

        }

        public void ResetGame()
        {
            string jsonStr = Resources.Load<TextAsset>(InitDataPath).text;
            _data = JsonConvert.DeserializeObject<GameData>(jsonStr);
            SaveGame();
        }

        public void SaveGame()
        {
            Debug.Log("Save Game: " + FilePath);
            string jsonstr = JsonConvert.SerializeObject(Data);
            //Writes to AppData file
            File.WriteAllText(FilePath, jsonstr);
        }

        public void LoadGame()
        {
            string jsonstr = "";
            if (File.Exists(FilePath))
                jsonstr = File.ReadAllText(FilePath);
            else
            {
                Debug.Log("No Load Found");
                ResetGame();
                return;
            }
            _data = JsonConvert.DeserializeObject<GameData>(jsonstr);
        }

        public SceneData LoadScene(string sceneName)
        {
            return Data.GetScene(sceneName);
        }

        public Inventory LoadInventory()
        {
            return Data.inventory;
        }
    }

    

    #region SCENE MANAGEMENT
    [Serializable]
    public class SVector3
    {
        public float x, y, z;
        public Vector3 GetVector()
        {
            return new Vector3(x, y, z);
        }

    }

    [Serializable]
    public class SceneObject
    {
        public string objectName;
        [NonSerialized]
        private GameObject _go;
        public GameObject GetObject()
        {
            if (_go == null)
            {
                _go = GameObject.Find(objectName);
            }
            return _go;
        }
        [HideInInspector]
        public bool active;
        [HideInInspector]
        public SVector3 position;
        [HideInInspector]
        public SVector3 rotation;

        public void Initialize()
        {
            GetObject().transform.position = position.GetVector();
            GetObject().transform.eulerAngles = rotation.GetVector();
            GetObject().SetActive(active);
        }
    }

    [Serializable]
    public class SceneData
    {
        public string SceneName;
        public int SceneIndex;
        public List<SceneObject> SceneObjects;

        public void Load()
        {
            foreach(var s in SceneObjects)
            {
                s.Initialize();
            }
        }
    }
    #endregion

    

    #region OPTIONS MANAGEMENT
    public class Options
    {
        public float MainVolume;
        public float MusicVolume;
        public float SFXVolume;
        public float DialogueVolume;
        public float DialogueSpeed;
        public float CameraSensitivity;
    }
    #endregion
}
