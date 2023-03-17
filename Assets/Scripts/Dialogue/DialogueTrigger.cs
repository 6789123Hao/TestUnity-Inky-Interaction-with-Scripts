using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;


    [SerializeField] private GameObject cue;

    private void Awake()
    {
        
    }

    private void Update()
    {
        //if (InputManager.GetInstance().GetSubmitPressed()) 
        if (Input.GetButtonUp("Say") && !DialogueManager.GetInstance().dialogueIsPlaying)
        {

            Debug.Log("TriggerPressed");
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
        }
        
    }

}
