using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour
{
    //References
    [SerializeField] private GameObject cursorObject;
    private InputManager inputManager;
    private CombatManager combatManager;

    private void Start()
    {
        //References
        inputManager = Object.FindObjectOfType<InputManager>();
        combatManager = Object.FindObjectOfType<CombatManager>();
    }

    //Initializing

    public void Initialize()
    {
        //Add self listeners
        inputManager.upButtonPressed.AddListener(MoveSelectionForward);
        inputManager.downButtonPressed.AddListener(MoveSelectionBackward);
        inputManager.submissionButtonPressed.AddListener(SubmitSelection);

        UpdateTargetList();
        selectionIndex = 0;
        DisplaySelection();
    }

    public void DeInitialize()
    {
        //Remove self listeners
        inputManager.upButtonPressed.RemoveListener(MoveSelectionForward);
        inputManager.downButtonPressed.RemoveListener(MoveSelectionBackward);
        inputManager.submissionButtonPressed.RemoveListener(SubmitSelection);
    }

    private List<Unit> targets = new List<Unit>();
    private void UpdateTargetList()
    {
        //Clear old units
        targets.Clear();

        //Add new units
        foreach (Unit target in Object.FindObjectsOfType<Unit>())
        {
            if (target.lifeStatus == Unit.LifeStatus.dead) continue;

            targets.Add(target);
        }

    }

    //Funcionality
    //---Changing Selection
    private int selectionIndex = 0;
    private void MoveSelectionForward()
    {
        IncreaseIndex();
        DisplaySelection();
    }

    private void MoveSelectionBackward()
    {
        DecreaseIndex();
        DisplaySelection();
    }

    private void SubmitSelection()
    {
        if (combatManager.state != CombatManager.State.player_targetSelection) return;

        GameObject targetObject = GetCurrentTarget().gameObject;
        combatManager.SubmitCharacterTargetSelection(targetObject);

        //tell combat mangaer to go next phase
        combatManager.NextPhase();
    }

    //---Display
    private void DisplaySelection()
    {
        GameObject targetObject = GetCurrentTarget().gameObject;
        cursorObject.transform.position = targetObject.transform.position;
    }

    private Unit GetCurrentTarget()
    {
        return targets.ToArray()[selectionIndex];
    }

    //---Changing Index
    private void IncreaseIndex()
    {
        int tempIndex = 1 + selectionIndex;

        if (selectionIndex == (targets.Count - 1) )
        {
            tempIndex = 0;
        }

        selectionIndex = tempIndex;
    }

    private void DecreaseIndex()
    {
        int tempIndex = -1 + selectionIndex;

        if (selectionIndex == 0)
        {
            tempIndex = targets.Count - 1;
        }

        selectionIndex = tempIndex;
    }

}
