using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines = new Queue<DialogueLine>();

    public bool isDialogueactive = false;
    public float typingspeed = 0.3f;
    
    public GameObject dialoguebox;
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
            Instance = this;
        
    }

    public void StartDialogue(Dialogue dialogue){
        isDialogueactive = true;
        dialoguebox.SetActive(true);
        lines.Clear();
        foreach(DialogueLine dialogueLine in dialogue.dialogueLines){
            lines.Enqueue(dialogueLine);
        }
        DisplayNextDialogueLine();

    }

    public void DisplayNextDialogueLine(){
        if(lines.Count == 0){
            EndDialogue();
            return;
        }
        DialogueLine currentLine = lines.Dequeue();
        characterName.text = currentLine.character.name;
        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine){
        dialogueArea.text = "";
        foreach(char letter in  dialogueLine.line.ToCharArray()){
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingspeed);
        }
    }

    public void EndDialogue(){
        isDialogueactive = false;
        dialoguebox.SetActive(false);
    }
}
