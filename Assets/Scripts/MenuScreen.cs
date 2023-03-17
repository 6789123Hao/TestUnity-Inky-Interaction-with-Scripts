using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MenuScreen : MonoBehaviour
{
    public float timeGap = 0.4f;
    public float currrentTime = 0f;
    public ModalPanelControl quitBox;
    public ModalPanelControl creditBox;
    public ModalPanelControl settingBox;
    public ModalPanelControl loadingBox;

    private bool isInControl = true;

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
    
    }

    public void PlayGame(){
        PlayerPrefs.SetInt("PreviousSceneIndex", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("GameScene");
    }

    public void GainControl() {
        isInControl = true;
    }

    private void Update()
    {
        if (isInControl)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                //if (!EventSystem.current.IsPointerOverGameObject()){
                    QuitPanelActive(); 
            }
        }
    }

    public void ConfirmQuit()
    {
        Application.Quit();
    }

    public void QuitPanelActive()
    {
        quitBox.gameObject.SetActive(true);
        isInControl = false;
    }
    public void CreditPanelActive()
    {
        isInControl = false;
        creditBox.gameObject.SetActive(true);
    }
    public void SettingPanelActive()
    {
        settingBox.gameObject.SetActive(true);
        isInControl = false;
    }
    public void LoadingPanelActive()
    {
        isInControl = false;
        loadingBox.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("firstGameofDay", 0);
        Debug.Log("DestoryMenu");
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("firstGameofDay", 0);
    }
}
