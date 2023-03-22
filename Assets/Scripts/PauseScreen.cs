using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    /*    public Timer timer;*/
    public DialogueManager diaManager;
    // Called when ever enable as active
    private void OnEnable()
    {
        Time.timeScale = 0;
        diaManager.SetStopListening();
        Debug.Log("PasueStropListenPls");
    }

    private void OnDisable()
    {
        Time.timeScale = 1;

        diaManager.SetStartListening();
        /*        timer.TimerActive(true);*/
    }

}
