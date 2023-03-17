using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using Ink;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    // Singouton Class.
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public bool dialogueIsPlaying { get; private set; }

    private Story currentStory;
    List<string> tags;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    static Choice choiceSelected;


    public static DialogueManager instance { get; private set; }

    private void Awake()
    {
        dialogueIsPlaying = false;
        if (instance != null) {
            Debug.LogWarning("SHOULDN'T HAVE MORE THAN ONE INSTANCES");
        }
        instance = this;

    }

    public static DialogueManager GetInstance() {
        return instance;
    }
    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }

    private void Update()
    {
        //return if no dialouge is playing
        if (!dialogueIsPlaying)
        {
            return;
        }
        //continue if next line is avalible and Say is pressed
        //if (InputManager.GetInstance().GetSubmitPressed()) {
        if (Input.GetButtonDown("Say"))
        {
            ContinueStory();
            Debug.Log("DiaManagerUpdatePressed");
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON) {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        Debug.Log("EnterDialogueMode");

        ContinueStory();
    }

    private void ContinueStory() {
        if (currentStory.canContinue)
        {
            Debug.Log("canContinue");
            AdvanceDiagloue();
            /*dialogueText.text = currentStory.Continue();*/
        }
        else
        {

            Debug.Log("ExitContinue");
            StartCoroutine(ExitDialogueMode());
        }
    }

    void AdvanceDiagloue() {
        string currentSentance = currentStory.Continue();
        Debug.Log("current = " + currentSentance);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentSentance));
    
    }

    IEnumerator TypeSentence(string sentence) {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }


        yield return null;
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        throw new NotImplementedException();
    }
}
