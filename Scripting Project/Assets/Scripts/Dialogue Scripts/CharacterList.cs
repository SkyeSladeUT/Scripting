using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace GameManagement
{
    [CreateAssetMenu(menuName = "Character List")]
    public class CharacterList : ScriptableObject
    {
        public List<Character> characters;
        public Character this[string characterName]
        {
            get { return characters.Find((n) => n.CharacterName == characterName ); }
        }
        public Character this[int index]
        {
            get { return characters[index]; }
        }
    }

    [Serializable]
    public class Character
    {
        public string CharacterName;
        public Color CharacterColor = Color.white;
        public string CharacterObjectName;
        public Color ThoughtColor
        {
            get 
            { 
                Color c = CharacterColor;
                c.a = .5f;
                return c;
            }
        }
    }
}
