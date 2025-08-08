using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptable Objects/DialogueData")]
public class DialogueData : ScriptableObject
{
    public string speakerName;
    public string[] dialogueLines;
    public Sprite speakerImage;
    public float typingSpeed = 0.05f;
}
