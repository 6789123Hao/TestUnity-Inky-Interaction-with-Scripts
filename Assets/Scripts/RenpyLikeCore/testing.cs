using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{
    public string inputLine;
    void Start()
    {
        //character = new Character("e", "Elieen", Color.red, "Elieen.jpg");
        InputDecoder.CharacterList.Add(new Character("e", "Elieen", Color.red, "Elieen.jpg"));

        inputLine = "e \"this is some text\"";
        InputDecoder.ParseInputLine(inputLine);
    }
}
