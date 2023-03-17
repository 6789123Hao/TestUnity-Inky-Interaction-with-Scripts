using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class ModalWindowPanel : MonoBehaviour
{
    [Header("Header")]
    [SerializeField] private Transform _headerArea;
    [SerializeField] private TextMeshProUGUI _titleField;


    [Header("Content")]
    [SerializeField] private Transform _contentArea;
    [Space()]
    [SerializeField] private Transform _verticalLayoutArea;
    [SerializeField] private Image _heroImage;
    [SerializeField] private TextMeshProUGUI _heroTextField;

    [Space()]
    [SerializeField] private Transform _horizontalLayoutArea;
    [SerializeField] private Transform _iconContainer;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _iconTextField;

    [Space()]
    [Header("Footer")]
    [SerializeField] private Transform _footerArea;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _declienButton;
    [SerializeField] private Button _alternateButton;

    //dynamic call back that can be passed into to methods
    private Action onConfirmAction;
    private Action onDeclineAction;
    private Action onAlternateAction;


    public void Confirm() {
        onConfirmAction?.Invoke();
        Close();
    }
    public void Decline()
    {
        onDeclineAction?.Invoke();
        Close();
    }
    public void Alternate()
    {
        onAlternateAction?.Invoke();
        Close();
    }

    private void Close() {
        gameObject.SetActive(false);
        throw new NotImplementedException();
    }
    private void Show()
    {
        throw new NotImplementedException();
    }
    public void ShowAsHeroVerticle(string title, Sprite imageToShow, string message,
        string confirmMessage, string declineMessage, string alterMessage,
         Action confirmAction, Action declineAction, Action alternateAction) 
    {
        _horizontalLayoutArea.gameObject.SetActive(false);
        _verticalLayoutArea.gameObject.SetActive(true);

        bool hasEmpty = string.IsNullOrEmpty(title);//check title exist
        Debug.Log("Title Empty: " + hasEmpty);
        _headerArea.gameObject.SetActive(!hasEmpty);
        _titleField.text = title;

        _heroImage.sprite = imageToShow;
        _heroTextField.text = message;

        onConfirmAction = confirmAction;

        hasEmpty = (declineAction != null);
        _declienButton.gameObject.SetActive(!hasEmpty);
        onDeclineAction = declineAction;

        hasEmpty = (alternateAction != null);
        _alternateButton.gameObject.SetActive(!hasEmpty);
        onAlternateAction = alternateAction;

        Show();
    }

    public void ShowAsHeroVerticle(string title, Sprite imageToShow, string message,
     Action confirmAction, Action declineAction)
    {
        ShowAsHeroVerticle(title, imageToShow, message, "Continue", "Back", "", confirmAction, declineAction, null);
    }
    public void ShowAsHeroVerticle(string title, Sprite imageToShow, string message,
     Action confirmAction)
    {
        ShowAsHeroVerticle(title, imageToShow, message, "Continue", "", "", confirmAction, null, null);
    }


}
