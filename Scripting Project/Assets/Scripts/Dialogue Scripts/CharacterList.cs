using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace Dialogue
{
    [CreateAssetMenu(menuName = "Character List")]
    public class CharacterList : ScriptableObject
    {
        public List<Character> characters;
    }

    [Serializable]
    public class Character
    {
        public string CharacterName;
        public Color CharacterColor = Color.white;
        public GameObject CharacterPrefab;

    }
}
