using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
/*    public Timer timer;*/

    // Called when ever enable as active
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
/*        timer.TimerActive(true);*/
    }
}
