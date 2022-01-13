using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameManagement
{
    [Serializable]
    [CreateAssetMenu(menuName = "Dialogue")]
    public class DialogueContainer : ScriptableObject
    {
        public SColor color;
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

