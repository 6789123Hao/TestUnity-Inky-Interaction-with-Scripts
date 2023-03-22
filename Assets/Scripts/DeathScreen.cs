using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
   public int deathSpeed;
   public int deathCounter;
   public Text poopReport;

   public Text textTimer;
   public GameObject BackGround;
   public GameObject DeathFace;
   public GameScreen gameScreen;

   //AD control
   public int MinGameBeforeAd;

   private string stringTime;
   private string highTime;
   private int CurPoo;
   public Timer timer;
   public float timeLeft;
   public Color targetColor;
   public bool NIGHTCLUB;
   private int Scoretemp;



   //awake called before starts
   private void Awake()
   {
      gameObject.SetActive(false);
      deathSpeed = 5;
      MinGameBeforeAd = 2;
      if (PlayerPrefs.GetInt("firstGameofDay", 0) == 0)
      {
         Debug.Log("first");
         PlayerPrefs.SetInt("firstGameofDay", 1);

         PlayerPrefs.SetInt("adCounter", 0);
      }
      else
      {
         Debug.Log("Notfirst");
      }


      NIGHTCLUB = (PlayerPrefs.GetInt("NightClub") != 0);
      Debug.Log("NightClub " + PlayerPrefs.GetInt("NightClub"));
      //PlayerPrefs.SetInt("NightClub", (NIGHTCLUB ? 1 : 0));
   }
   // Called when ever enable as active
   private void OnEnable()
   {
      timer.TimerActive(false);
      DeathFace.SetActive(true);
      targetColor = new Color(0, 0, 0);
      Time.timeScale = deathSpeed;



      //get poo count, min = 1; update poo text and 
      //CurPoo = gameScreen.GetTotalPoo();
      //poopReport.text = CurPoo.ToString();

      stringTime = textTimer.text;
      highTime = PlayerPrefs.GetString("highTime", "00");
      if (stringIsBiggerForTime(stringTime, highTime))
      {
         PlayerPrefs.SetString("highTime", stringTime);
      }


      //update highscore and total poo
      Scoretemp = PlayerPrefs.GetInt("highScore", 0);
/*      if (CurPoo > Scoretemp)
      {
         PlayerPrefs.SetInt("highScore", CurPoo);
      }*/
      //Scoretemp = PlayerPrefs.GetInt("TotalPoo", 0);
      //Scoretemp += CurPoo;
      PlayerPrefs.SetInt("TotalPoo", Scoretemp);

      Debug.Log("OnEnableDeathAdCount" +
                          PlayerPrefs.GetInt("adCounter", 0));

   }

   private void Update()
   {
      //BackGround.GetComponent<Renderer>().material.color = targetColor;

      if (timeLeft <= Time.deltaTime)
      {
         // transition complete
         // assign the target color
         BackGround.GetComponent<Renderer>().material.color = targetColor;

         // start a new transition
         // Color changes
         if (NIGHTCLUB)
         {
            targetColor = new Color(Random.value, Random.value, Random.value);
            timeLeft = 1.0f;
         }

      }
      else
      {
         // transition in progress
         // calculate interpolated color
         BackGround.GetComponent<Renderer>().material.color =
             Color.Lerp(BackGround.GetComponent<Renderer>().material.color,
             targetColor, Time.deltaTime / timeLeft);

         // update the timer
         timeLeft -= Time.deltaTime;
      }
   }

   private void OnDisable()
   {
      timer.TimerActive(true);
      DeathFace.SetActive(false);
      Time.timeScale = 1;
   }


   //reload the scene/level
   public void RestartGame()
   {
      checkAdGoToScene(SceneManager.GetActiveScene().name);
   }

   public void GoToMenu()
   {
      checkAdGoToScene("MenuScene");
   }

   public void GoToShare()
   {
      SceneManager.LoadScene("SocialMediaScene");
   }

   public void GoToAds()
   {
      if (Application.internetReachability == NetworkReachability.NotReachable)
      {
         Debug.Log("Error. Check internet connection!");
      }
      SceneManager.LoadScene("AdScene");
   }

   private void checkAdGoToScene(string SceneName)
   {

      deathCounter = PlayerPrefs.GetInt("adCounter", 0);
      Debug.Log("checkAddeathCounter" + deathCounter);
      deathCounter++;

      if (deathCounter > MinGameBeforeAd)
      {

         PlayerPrefs.SetInt("adCounter", 0);
         SceneManager.LoadScene("AdScene");

      }
      else
      {
         PlayerPrefs.SetInt("adCounter", deathCounter);
         SceneManager.LoadScene(SceneName);
      }
   }

   private bool stringIsBiggerForTime(string a, string b)
   {
      int lengthA = a.Length;
      int lengthB = b.Length;
      if (lengthA > lengthB)
      {
         return true;
      }
      if (lengthA == lengthB)
      {
         for (int i = 0; i < lengthA; i++)
         {
            if (a[i] > b[i])
            {
               return true;
            }
         }
      }


      return false;
   }


}
