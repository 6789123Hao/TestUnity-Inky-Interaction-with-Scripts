using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//to let Inspecter able to view
public class CharacterScriptableContainer
{
    public static int rankId;
    public string nameShort;
    public string charName;
    public Sprite iconSpriteDefault;
    public Sprite characterSpriteDefault;
    public GameObject displayBodyPanel;
    public bool status;
    public int specializedStatusType;
    public float relationStatus;


    public CharacterScriptableContainer (int rank, Sprite iconSprite){
        rankId = rank;
        iconSpriteDefault = iconSprite;
        characterSpriteDefault = null;
        status = false;
        specializedStatusType = 0;
        relationStatus = Mathf.NegativeInfinity;

    }
    public CharacterScriptableContainer(string path)
    {
        //for customiconSprite
        characterSpriteDefault = Resources.Load<Sprite>(path);

    }
    public CharacterScriptableContainer(Sprite iconSprite, Sprite bodySprite = null)
    {
        characterSpriteDefault = bodySprite;
        iconSpriteDefault = iconSprite;
    }
}
