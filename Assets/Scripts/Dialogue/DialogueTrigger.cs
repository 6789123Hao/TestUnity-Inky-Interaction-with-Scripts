using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private void Awake()
    {
        
    }

    public IEnumerator decideStory() {
        yield return new WaitForSeconds(0.1f);
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);

    }

}
