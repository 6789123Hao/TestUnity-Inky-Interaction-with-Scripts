using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameScreen : MonoBehaviour
{
    //public TMP_Text tmpText;
    public PauseScreen pauseScreen;
/*    public DeathScreen deathScreen;
    public Timer timer;*/
    public int TotalPooCount;
/*    private bool death;
*/
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseGame();
        }
        //tmpText.text = TotalPooCount.ToString();
    }

    private void Start()
    {
        //death = false;
        pauseScreen.gameObject.SetActive(false);
    }



    public void PauseGame() {


        //timer.TimerActive(false);
        //trun default offscreen on
        pauseScreen.gameObject.SetActive(true);


    }

    public void GoToMenu()
    {
        checkAdGoToScene("MenuScene");
    }
    private void checkAdGoToScene(string SceneName)
    {

        //deathCounter = PlayerPrefs.GetInt("adCounter", 0);
        //Debug.Log("checkAddeathCounter" + deathCounter);
       // deathCounter++;

        //if (deathCounter > MinGameBeforeAd)
        //{

            //PlayerPrefs.SetInt("adCounter", 0);
            //SceneManager.LoadScene("AdScene");

        //}
        //else
        //{
            //PlayerPrefs.SetInt("adCounter", deathCounter);
            SceneManager.LoadScene(SceneName);
       // }
    }
    private void OnApplicationPause(bool pause)
    {
        PauseGame();
    }

    private void OnEnable()
    {
        //timer.TimerActive(true);
    }
    private void OnDisable()
    {
        //if (!death)
        {
            //timer.TimerActive(false);
            //trun default offscreen on
            pauseScreen.gameObject.SetActive(true);
        }
    }

    

    public void OnPlayerDeath()
    {
        //death = true;
/*        PlayerPrefs.SetInt("TotalDeath", 1 + 
                            PlayerPrefs.GetInt("TotalDeath", 0));*/
       // timer.TimerActive(false);
        gameObject.SetActive(false);
       // deathScreen.gameObject.SetActive(true);

    }

}
