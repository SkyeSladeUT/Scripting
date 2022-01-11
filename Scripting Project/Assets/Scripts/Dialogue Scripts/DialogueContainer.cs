using System.Collections.Generic;
using UnityEngine;

namespace GameManagement
{
    [SerializeField]
    [CreateAssetMenu(menuName = "Dialogue")]
    public class DialogueContainer : ScriptableObject
    {
        public Color ChoiceColor;
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

