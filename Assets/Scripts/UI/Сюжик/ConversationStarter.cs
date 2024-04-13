using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    void Start()
    {
        ConversationManager.Instance.StartConversation(myConversation);
    }
}
