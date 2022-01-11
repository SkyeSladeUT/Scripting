using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace GameManagement
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Data/Data")]
    public class GameData : ScriptableObject
    {
        public int currentScene;
        public List<SceneData> Scenes;
        public Inventory masterInventory;
        [HideInInspector]
        public Inventory inventory;
        [HideInInspector]
        public Options options;
        public DialogueContainer dialogues;


        public SceneData GetScene(string sceneName)
        {
            return Scenes.FirstOrDefault(s => s.SceneName == sceneName);
        }
    }
}
