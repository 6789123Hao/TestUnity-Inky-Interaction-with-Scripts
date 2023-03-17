using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;

public class HeroWindowTrigger : MonoBehaviour
{
    public string title;
    public Sprite sprite;
    public string message;
    public bool triggerOnEnable = true;


    public UnityEvent onContinueEvent;
    public UnityEvent onCancelEvent;
    public UnityEvent onAlternateEvent;

    private void OnEnable()
    {
        if (!triggerOnEnable) { return; }
        Debug.Log("HeroEnabled");

        Action continueCallback = null;
        Action cancelCallback = null;
        Action alternateCallback = null;

        if (onContinueEvent.GetPersistentEventCount() > 0) {
            continueCallback = onContinueEvent.Invoke;
        }
        if (onCancelEvent.GetPersistentEventCount() > 0)
        {
            cancelCallback = onContinueEvent.Invoke;
        }
        if (onAlternateEvent.GetPersistentEventCount() > 0)
        {
            alternateCallback = onCancelEvent.Invoke;
        }

        UIController.instance.modalWindow.ShowAsHeroVerticle(title, sprite, message, 
            "continue", "Nah", "skip?",
            continueCallback, cancelCallback, alternateCallback);
    }
}
