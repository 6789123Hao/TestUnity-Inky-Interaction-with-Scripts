using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalPanelControl : MonoBehaviour
{
    /*    public float timeGap = 0.4f;
        public float currrentTime = 0f;*/
    public MenuScreen parentPanel;

    // Start is called before the first frame update
    void Awake()
    {
       
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("ESC");
            CancelModal();
/*            currrentTime = timeGap;*//**/
        }
        // Only update the time if the if block wasn't executed
/*        if (currrentTime > 0)
        {
            currrentTime -= Time.deltaTime;
        }*/
    }


    public void CancelModal() {
        gameObject.SetActive(false);
        parentPanel.GainControl();
    }

}
