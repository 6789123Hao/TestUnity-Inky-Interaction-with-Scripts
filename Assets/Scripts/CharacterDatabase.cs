using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDatabase : ScriptableObject
{

    public CharacterScriptableContainer[] character;

    public int CharacterCount
    {
        get
        {
            return character.Length;
        }
    }

    public void cleanUpNull()
    {
        CharacterScriptableContainer[] tempArray =
            new CharacterScriptableContainer[character.Length];
        int newSize = 0;
        for (int i = 0; i < character.Length; i++)
        {
/*            Debug.Log(i + " " + (character[i].characterSprite == null));
            if (character[i].characterSprite != null)
            {
                tempArray[newSize] = character[i];
                newSize++;
            }*/

        }
        if (newSize != character.Length)
        {
            character = new CharacterScriptableContainer[newSize];
            for (int i = 0; i < newSize; i++)
            {
                character[i] = tempArray[i];
            }
        }

    }

    public CharacterScriptableContainer GetCharacter(int index)
    {
        if (character.Length > index)
        {
            return character[index];
        }
        else
        {
            return null;
        }
    }

    public void AddCharacter(Sprite sprite)
    {

        if (character.Length > 10)
        {
            cleanUpNull();
        }

        CharacterScriptableContainer[] newArray =
            new CharacterScriptableContainer[character.Length + 1];
        character.CopyTo(newArray, 0);
        newArray[character.Length] = character[character.Length - 1];

        //change second to last item to new image
        newArray[character.Length - 1] = new CharacterScriptableContainer(sprite);

        character = newArray;

    }

    public void AddCharacter(string path)
    {

        CharacterScriptableContainer[] newArray =
            new CharacterScriptableContainer[character.Length + 1];
        character.CopyTo(newArray, 0);
        newArray[character.Length] = character[character.Length - 1];

        //change second to last item to new image
        newArray[character.Length - 1] = new CharacterScriptableContainer(path);

        character = newArray;

    }

    public void deleteCharacter(int index)
    {
        CharacterScriptableContainer[] newArray =
            new CharacterScriptableContainer[character.Length - 1];
        for (int i = 0; i < index; i++)
        {
            newArray[i] = character[i];
        }
        for (int i = index; i < character.Length; i++)
        {
            newArray[i - 1] = character[i];
        }

        newArray[character.Length] = character[character.Length - 1];

        character = newArray;
    }
}
