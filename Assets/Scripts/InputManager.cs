using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    //Events
    public UnityEvent submissionButtonPressed = new UnityEvent();
    public UnityEvent upButtonPressed = new UnityEvent();
    public UnityEvent downButtonPressed = new UnityEvent();

    private void Update()
    {
        DetectInputs();
    }

    private void DetectInputs()
    {
        DetectSubmissionButton();
        DetectUpButton();
        DetectDownButton();
    }

    private void DetectSubmissionButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            submissionButtonPressed.Invoke();
        }
    }

    private void DetectUpButton()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upButtonPressed.Invoke();
        }
    }

    private void DetectDownButton()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downButtonPressed.Invoke();
        }
    }

}
