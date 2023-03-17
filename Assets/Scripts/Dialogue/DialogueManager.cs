using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using Ink;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    // Singouton Class.
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    public bool dialogueIsPlaying { get; private set; }

    public GameObject customButton;
    public GameObject optionPanel;

    private Story currentStory;
    List<string> tags;

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI nameText;
    static Choice choiceSelected;
    private bool canContinueToNextLine;

    public static DialogueManager instance { get; private set; }

    private void Awake()
    {
        canContinueToNextLine = true;
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

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }

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
        if (optionPanel.activeInHierarchy)
        {
            Debug.Log("option Open");
        } 
        else 
        {
            if (currentStory.currentChoices.Count == 0 && Input.GetButtonDown("Say")
                && dialogueIsPlaying)
            {
                ContinueStory();
                Debug.Log("DiaManagerUpdatePressed");
            }
        }


    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        dialogueIsPlaying = true;
        currentStory = new Story(inkJSON.text);
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
            if (currentStory.currentChoices.Count != 0) {
                StartCoroutine(ShowChoices());
            }
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
        dialogueText.text = sentence;
        dialogueText.maxVisibleCharacters = 0;

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.maxVisibleCharacters++;
            yield return null;
        }


        yield return null;
    }

    private IEnumerator ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        yield return new WaitForSeconds(0.2f);
    }


    IEnumerator ShowChoices() {
        Debug.Log("ChoiceTime");
        List<Choice> currentChoices = currentStory.currentChoices;
        if (currentChoices.Count > choices.Length) {
            Debug.LogError("More choices were given than UI can support. choices #:" +
                currentChoices.Count);
        }

        int index = 0;
        foreach (Choice choice in currentChoices) {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++) {
            choices[i].gameObject.SetActive(false);
        }

/*        for (int i = 0; i < _choices.Count; i++)
        {
            GameObject temp = Instantiate(customButton, optionPanel.transform);
            if (temp != null)
            {
                temp.transform.GetChild(0).GetComponent<Text>().text = _choices[i].text;
                temp.AddComponent<Selectable>();
                temp.transform.GetChild(0).GetComponent<Text>().text = _choices[i].text;
                temp.GetComponent<Button>().interactable = true;
                temp.GetComponent<Button>().onClick.AddListener(() => SetDecision(-[i]));
            }
        }*/
        if (optionPanel != null)
        {

            Debug.Log("optionPanel != null");
            optionPanel.SetActive(true);
            yield return new WaitUntil(() => { return choiceSelected != null; });
        }
        AdvanceFromDecision();
    }
/*
        // Tells the story which branch to go to
        public void SetDecision(object element)
        {
            choiceSelected = (Choice)element;
            currentStory.ChooseChoiceIndex(choiceSelected.index);
        }*/

    private void AdvanceFromDecision()
    {
        optionPanel.SetActive(false);
        for (int i = 0; i < optionPanel.transform.childCount; i++)
        {
            Destroy(optionPanel.transform.GetChild(i).gameObject);
        }
        choiceSelected = null;

        AdvanceDiagloue();
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        // NOTE: The below two lines were added to fix a bug after the Youtube video was made
        InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script
        ContinueStory();
    }
    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }
}
