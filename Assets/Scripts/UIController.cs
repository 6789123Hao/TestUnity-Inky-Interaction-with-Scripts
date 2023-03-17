using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//singleton
public class UIController : MonoBehaviour
{
    public static UIController instance;
    [SerializeField]
    private ModalWindowPanel _modalWindow;
    public ModalWindowPanel modalWindow => _modalWindow;

    private void Awake()
    {
        instance = this;
        Debug.Log("instanceMade");
    }
}
