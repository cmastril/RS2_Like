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
    public UnityEvent rightButtonPressed = new UnityEvent();
    public UnityEvent leftButtonPressed = new UnityEvent();

    private void Update()
    {
        DetectInputs();
    }

    private void DetectInputs()
    {
        DetectSubmissionButton();

        DetectUpButton();
        DetectDownButton();
        DetectRightButton();
        DetectLeftButton();
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

    private void DetectRightButton()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            upButtonPressed.Invoke();
        }
    }

    private void DetectLeftButton()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            upButtonPressed.Invoke();
        }
    }

}
