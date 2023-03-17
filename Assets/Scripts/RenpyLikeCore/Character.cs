using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    public string shortName;
    public string fullName;
    public Color color;
    public string sideImage;

    public Character(string shortNameInput, string fullNameInput, Color colorInput, string sideImageInput) 
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.sideImage = sideImageInput;

        CheckNamesNull();
        
    }



    public Character(string shortNameInput, string fullNameInput, Color colorInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = colorInput;
        this.sideImage = null;
        CheckNamesNull();

    }
    public Character(string shortNameInput, string fullNameInput)
    {
        this.shortName = shortNameInput;
        this.fullName = fullNameInput;
        this.color = Color.white;
        this.sideImage = null;
        CheckNamesNull();

    }

    private void CheckNamesNull() {
        if (this.fullName == null)
        {
            throw new InvalidPropertyException("Character FullName Null");
        }
        if (this.shortName == null)
        {
            throw new InvalidPropertyException("Character ShortName Null");
        }
    }

}
