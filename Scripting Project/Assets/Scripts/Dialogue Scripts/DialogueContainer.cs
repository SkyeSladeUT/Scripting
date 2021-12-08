using System.Collections.Generic;
using UnityEngine;
using static Dialogue.DialogueClasses;

namespace Dialogue
{
    [SerializeField]
    [CreateAssetMenu(menuName = "Dialogue")]
    public class DialogueContainer : ScriptableObject
    {
        public List<Conversation> conversations;

        public Conversation this[string conversationName]
        {
            get { return conversations.Find((n) => n.ConversationName == conversationName); }
        }

        public Conversation this[int conversationInt]
        {
            get { return conversations[conversationInt]; }
        }
    }
    
}

