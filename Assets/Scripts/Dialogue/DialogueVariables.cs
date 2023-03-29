using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVariables
{

    public Dictionary<string, Ink.Runtime.Object> variables { get; private set; }

    private Story globalVariablesStory;
    private const string saveVariablesKey = "INK_VARIABLES";

    public DialogueVariables(TextAsset loadingGlobalsJSON) 
    {
        //create story
        globalVariablesStory = new Story(loadingGlobalsJSON.text);
        //check saved data exist
        if (PlayerPrefs.HasKey(saveVariablesKey)) {

            string jsonState = PlayerPrefs.GetString(saveVariablesKey);
            globalVariablesStory.state.LoadJson(jsonState);
        }

        //initialize dictionary
        variables = new Dictionary<string, Ink.Runtime.Object>();
        foreach (string name in globalVariablesStory.variablesState) {

            Ink.Runtime.Object value = 
                globalVariablesStory.variablesState.GetVariableWithName(name);
            variables.Add(name, value);
            Debug.Log("Initialized GB variable: " + name + " = " + value);
        }
    }

    //variable observer function
    private void VariableChanged(string name, Ink.Runtime.Object value) 
    {

        Debug.Log("Variable changed: " + name + " " + value);
        if (variables.ContainsKey(name)) {

            variables.Remove(name);
            variables.Add(name, value);
        }
        
    }

    public void SaveVariables() {

        if (globalVariablesStory != null) {
            //Load state of all cur variables to global
            VariablesToStory(globalVariablesStory);
            //save global tojson to the unity playerprefs.
            PlayerPrefs.SetString(saveVariablesKey, globalVariablesStory.state.ToJson());
        }
    
    }

    private void VariablesToStory(Story story) {
        foreach (KeyValuePair<string, Ink.Runtime.Object> variable in variables) {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story);
        story.variablesState.variableChangedEvent += VariableChanged;


    }
    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChanged;


    }


}
