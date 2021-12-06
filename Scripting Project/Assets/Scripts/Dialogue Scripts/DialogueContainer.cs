using System.Collections.Generic;
using UnityEngine;
using static Dialogue.DialogueClasses;

namespace Dialogue
{
    [CreateAssetMenu(menuName = "Dialogue")]
    public class DialogueContainer : ScriptableObject
    {
        public int MaxCharactersPerLine;
        public float scrollSpeed;
        public List<Conversation> conversations;
    }
    
}

