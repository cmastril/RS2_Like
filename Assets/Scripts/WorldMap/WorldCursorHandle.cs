using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class WorldCursorHandle : MonoBehaviour
{
    //Events
    public UnityEvent selectionMoved = new UnityEvent();

    //References
    private InputManager inputManager;

    //Fields
    public WorldNode currentSelection;

    private void Start()
    {
        //Get references
        inputManager = Object.FindObjectOfType<InputManager>();

        //Set Listeners
        inputManager.rightButtonPressed.AddListener(MoveSelectionRight);
        inputManager.leftButtonPressed.AddListener(MoveSelectionLeft);
        inputManager.upButtonPressed.AddListener(MoveSelectionUp);
        inputManager.downButtonPressed.AddListener(MoveSelectionDown);
        inputManager.submissionButtonPressed.AddListener(LoadCurrentRegion);

        //Initialize
        Initialize();
    }

    [SerializeField] private WorldNode startNode;
    private void Initialize()
    {
        SetSelection(startNode);
    }

    private void MoveSelectionRight()
    {
        WorldNode rightReference = currentSelection.rightReference;

        //Check if can move
        if (rightReference == null) return;

        SetSelection(rightReference);
    }

    private void MoveSelectionLeft()
    {
        WorldNode leftReference = currentSelection.leftReference;

        //Check if can move
        if (leftReference == null) return;

        SetSelection(leftReference);
    }

    private void MoveSelectionUp()
    {
        WorldNode upReference = currentSelection.upReference;

        //Check if can move
        if (upReference == null) return;

        SetSelection(upReference);
    }

    private void MoveSelectionDown()
    {
        WorldNode downReference = currentSelection.downReference;

        //Check if can move
        if (downReference == null) return;

        SetSelection(downReference);
    }

    private void LoadCurrentRegion()
    {
        string sceneString = currentSelection.regionSceneName;
        SceneManager.LoadScene(sceneString);
    }

    private void SetSelection(WorldNode newNode)
    {
        currentSelection = newNode;
        selectionMoved.Invoke();
    }

}
