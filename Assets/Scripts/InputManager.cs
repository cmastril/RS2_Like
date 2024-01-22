using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    //Singleton
    private static InputManager inputManager = null;
    void Awake()
    {
        if (inputManager == null)
        {
            inputManager = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

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
        DetectDirectionKeys();
    }

    private void DetectDirectionKeys()
    {
        //Key Down Detection
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            upButtonPressed.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            downButtonPressed.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rightButtonPressed.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            leftButtonPressed.Invoke();
        }
    }

    private void DetectSubmissionButton()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            submissionButtonPressed.Invoke();
        }
    }

}
