using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.IO;

public class InputDecoder 
{
    public static List<Character> CharacterList = new List<Character>();


    //upon recieving string, we clean up the string
    //if it's direct "" quotes, Call Say function with it
    //if it has a name, Call splitToSay that deals with the Name display of name
    public static void ParseInputLine(string StringToParse) { 
        string withOutTabs = StringToParse.Replace("\t", "");
        StringToParse = withOutTabs;

        if (StringToParse.StartsWith("\"")) {
            Say(StringToParse);
        }

        string[] SeparatingString = { " ", "'", "\"", "(", ")" };
        string[] args = StringToParse.Split(SeparatingString, StringSplitOptions.RemoveEmptyEntries);
        foreach (Character character in CharacterList) {
            if (args[0] == character.shortName) {
                SplitToSay(StringToParse, character);
            }
        }

    }


#region Say Stuff

    public static void Say(string what)
    {
        Debug.Log(what);

    }
    public static void Say(string who, string what)
    {
        Debug.Log(who + ": " + what);
    }
    public static void SplitToSay(string StringToParse, Character character)
    {
        int toQuote = StringToParse.IndexOf("\"") + 1;
        int endQuote = StringToParse.Length - 1;
        string StringToOutput = StringToParse.Substring(toQuote, endQuote - toQuote);
        Say(character.fullName, StringToOutput);
    }


#endregion

}
