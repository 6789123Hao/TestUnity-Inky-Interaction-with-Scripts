using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class testcode : MonoBehaviour
{
    [SerializeField] private TextAsset loadGlobalsJSON;
    // Start is called before the first frame update
    public void ToFive()
    {

        Story story = new Story(loadGlobalsJSON.text);
        Debug.Log("test: " + story.variablesState["games_played"]);
        story.EvaluateFunction("changeGlobalPlayed", 5);

        Debug.Log("test: " + story.variablesState["games_played"]);
    }
}
