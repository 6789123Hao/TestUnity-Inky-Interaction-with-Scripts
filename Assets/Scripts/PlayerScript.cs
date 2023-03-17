using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private bool isInControl = true;

    public ModalPanelControl pausePanel;

    public DialogueTrigger trigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Cancel"))
        {
            //if (!EventSystem.current.IsPointerOverGameObject()){
            PausePanelActive();
        }
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        if (Input.GetButtonDown("Say"))
        {
            StartCoroutine(trigger.decideStory());
            Debug.Log("TriggerPressed");
                
        }


    }

    public void PausePanelActive()
    {
        pausePanel.gameObject.SetActive(true);
        isInControl = false;
    }
}
