using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dialoguetrigger : MonoBehaviour
{

    public Dialogue dialogue;
    public void triggerdialogue(){
        DialogueManager.Instance.StartDialogue(dialogue);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            triggerdialogue();
        }
    }
}

[System.Serializable]
public class DialogueCharacter
{
    public string name;

}

[System.Serializable]
public class DialogueLine 
{

    public DialogueCharacter character;
    [TextArea(3,10)]
    public string line;
}
[System.Serializable]
public class Dialogue 
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();

}
