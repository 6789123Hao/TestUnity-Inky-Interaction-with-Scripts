using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableListenerExample : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color happyColor = Color.green;
    [SerializeField] private Color sadColor = Color.blue;
    [SerializeField] private Color shockColor = Color.green;


    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        string globalString = ((Ink.Runtime.StringValue)DialogueManager
            .GetInstance()
            .GetVariableState("random_line")).value;

        switch (globalString)
        {
            case "":
                spriteRenderer.color = defaultColor;
                break;
            case "happy":
                spriteRenderer.color = happyColor;
                break;
            case "sad":
                break;
            case "shock":
                break;

            default:
                Debug.Log("error on globalString" + globalString);
                break;
        }
    }

}
