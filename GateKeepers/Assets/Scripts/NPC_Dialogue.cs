using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName = "NPCDialogue")]
public class NPC_Dialogue : ScriptableObject
{
    public string npcName;
    //public Image targetImage;
    public string[] dialogueLines;
    public float typingSpeed = 0.05f;
    //public float voicePitch = 1f;
}
