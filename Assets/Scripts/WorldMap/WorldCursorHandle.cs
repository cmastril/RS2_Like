using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCursorHandle : MonoBehaviour
{
    //References
    private InputManager inputManager;

    //Fields
    public WorldNode currentSelection;

    private void Start()
    {
        //Get references
        inputManager = Object.FindObjectOfType<InputManager>();

        //Set Listeners
        Debug.Log(inputManager);
        inputManager.rightButtonPressed.AddListener(MoveSelectionRight);
        inputManager.leftButtonPressed.AddListener(MoveSelectionLeft);

        //Initialize
        Initialize();
    }

    private void Initialize()
    {
        currentSelection = GameObject.FindObjectOfType<WorldNode>();
    }

    private void MoveSelectionRight()
    {
        WorldNode rightReference = currentSelection.rightReference;

        //Check if can move
        if (rightReference == null) return;

        currentSelection = rightReference;
    }

    private void MoveSelectionLeft()
    {
        WorldNode leftReference = currentSelection.leftReference;

        //Check if can move
        if (leftReference == null) return;

        currentSelection = leftReference;
    }

}
