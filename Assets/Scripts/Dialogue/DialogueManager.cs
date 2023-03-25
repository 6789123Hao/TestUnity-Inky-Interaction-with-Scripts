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
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;

    [SerializeField] private GameObject textPanel;

    [SerializeField] private GameObject choicePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    [SerializeField] private TextMeshProUGUI SPEAKERText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private Animator layoutAnimator;

    private AnimatorStateInfo animatorStateInfo;
    private AnimatorStateInfo layoutAnimatorStateInfo;

    [Header("Body Up UI")]
    [SerializeField] private Image VerticalCharPanel;


    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;




    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }
    public bool listening { get; private set; }

    private static DialogueManager instance;

    List<string> tags;
    private string[] splitTag;
    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portait";

    private const string LAYOUT_TAG = "layout";

    private const string Panel_TAG = "panel";
    private const string CHAR_PANEL_TAG = "bodyup";



    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;
        listening = true;
    }

    [Obsolete]
    public void HideUnhideDialogueBox() {
        if (dialoguePanel.activeSelf)
        {
            animatorStateInfo = portraitAnimator.GetCurrentAnimatorStateInfo
                (portraitAnimator.GetLayerIndex("Base Layer"));
            layoutAnimatorStateInfo = layoutAnimator.GetCurrentAnimatorStateInfo
                (portraitAnimator.GetLayerIndex("Base Layer"));

            dialoguePanel.SetActive(false);
        }
        else {

            dialoguePanel.SetActive(!dialoguePanel.activeSelf);
            portraitAnimator.Play(animatorStateInfo.nameHash, 
                portraitAnimator.GetLayerIndex("Base Layer"));
            layoutAnimator.Play(layoutAnimatorStateInfo.nameHash,
                layoutAnimator.GetLayerIndex("Base Layer"));

        }

    }
    public void SetStopListening()
    {
        listening = false;
    }
    public void SetStartListening()
    {
        listening = true;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        layoutAnimator = textPanel.GetComponent<Animator>();
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
       
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying || !listening)
        {
            return;
        }

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (currentStory.currentChoices.Count == 0 && Input.GetButtonDown("Say")
            && dialoguePanel.activeSelf)
        {
            Debug.Log("SayFromDialogue");
            ContinueStory();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Mouse1Pressed");
            if (dialogueIsPlaying)
            {
                HideUnhideDialogueBox();
            }
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        choicePanel.SetActive(false);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        dialoguePanel.SetActive(false);
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            dialogueText.text = currentStory.Continue();
            // display choices, if any, for this dialogue line


            HandleTags(currentStory.currentTags);
            DisplayChoices();
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        if (currentTags == null) {
            Debug.Log("Null Tags");
            return;
        }

        Debug.Log("currentTags: " + currentTags.ToString());
        // Loopthrough each tag
        foreach (string tag in currentTags) {
            splitTag = tag.Split(':');

            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey.ToLower())
            {
                default:
                    Debug.LogWarning("Tag came in but is not being handled: " + tag);
                    break;
                case SPEAKER_TAG:
                    Debug.Log(tagValue + " the SPEAKER_TAG");
                    if (tagValue == "")
                    {
                        layoutAnimator.Play("noName");
                    }
                    else {
                        layoutAnimator.Play("withNamewithFace");
                    }
                    SetName(tagValue);
                    break;
                case PORTRAIT_TAG:
                    Debug.Log(tagValue + " the PORTRAIT_TAG");
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    Debug.Log(tagValue + " the LAYOUT_TAG");
                    layoutAnimator.Play(tagValue);
                    break;
                case CHAR_PANEL_TAG:
                    SetBodyUpImage(tagValue);
                    break;
            }
        }
    }

    private void SetBodyUpImage(string tagValue)
    {
        
    }

    private void SetName(string tagValue)
    {
        SPEAKERText.text = tagValue;
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentChoices.Count);
        }
        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        choicePanel.SetActive(true);
        //StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {

        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();

        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        choicePanel.SetActive(false);
        // NOTE: The below two lines were added to fix a bug after the Youtube video was made
        InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script

        
        ContinueStory();
    }
}
