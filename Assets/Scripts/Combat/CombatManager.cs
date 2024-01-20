using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    //References
    PartyManager partyManager;

    private void Start()
    {
        partyManager = Object.FindObjectOfType<PartyManager>();

        NextPhase();
    }

    //State
    public enum State {initialization, player_abilitySelection, player_targetSelection, enemy_selection, combat}
    public State state = State.combat;

    //Phase Manager Objects
    //---Ability Manager
    [SerializeField] private GameObject combatMenu;
    //---Target Selection Manager
    [SerializeField] private GameObject selectionManager;

    public void NextPhase()
    {
        if (state == State.initialization)
        {
            //Change State
            state = State.player_abilitySelection;

            //Handle Managers
            combatMenu.SetActive(true);
            combatMenu.GetComponent<Menu_Combat>().Initialize();

            selectionManager.SetActive(false);
            selectionManager.GetComponent<TargetSelection>().DeInitialize();

            //Reset
            Reset();
        }
        else if (state == State.player_abilitySelection)
        {
            //Change State
            state = State.player_targetSelection;

            //Handle Managers
            combatMenu.SetActive(false);
            combatMenu.GetComponent<Menu_Combat>().Deinitialize();

            selectionManager.SetActive(true);
            selectionManager.GetComponent<TargetSelection>().Initialize();
        }
        else if (state == State.player_targetSelection && (isPartyMemberSelectionComplete() == false))
        {
            //Change State
            state = State.player_abilitySelection;

            //Handle Managers
            combatMenu.SetActive(true);
            combatMenu.GetComponent<Menu_Combat>().Initialize();

            selectionManager.SetActive(false);
            selectionManager.GetComponent<TargetSelection>().DeInitialize();

            //Change Character
            GetNextCharacter();
        }
        else if (state == State.player_targetSelection && (isPartyMemberSelectionComplete() == true))
        {
            //Change State
            state = State.enemy_selection;

            Debug.Log("Input code to handle Phase: target_selection -> enemy_ai");
        }


        if (state == State.combat)
        {
            //Change State
            state = State.player_abilitySelection;

            //Handle Managers
            combatMenu.SetActive(true);
            combatMenu.GetComponent<Menu_Combat>().Initialize();

            selectionManager.SetActive(false);
            selectionManager.GetComponent<TargetSelection>().DeInitialize();


            //Reset
            Reset();
        }
    }

    //MOVE LATER 
    private bool isPartyMemberSelectionComplete()
    {
        int numOfPossiblePartyMembers = 0;
        foreach (Unit unit in Object.FindObjectsOfType<Unit>())
        {
            if (unit.owner == Unit.Owner.player && unit.lifeStatus == Unit.LifeStatus.alive)
            {
                numOfPossiblePartyMembers++;
            }
        }

        int numOfStoredMemberActions = characterToAbilityDict.Count;

        if (numOfPossiblePartyMembers == numOfStoredMemberActions)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Going to next phase");
            NextPhase();
        }
    }

    //Character Selection Data
    public Unit currentUnitSelected;

    private void GetNextCharacter()
    {
        //Get list of possible party memebers to act
        List<Unit> availableUnits = new List<Unit>();
        foreach (Unit partyUnit in partyManager.partyUnits)
        {
            if (partyUnit.lifeStatus == Unit.LifeStatus.alive)
            {
                availableUnits.Add(partyUnit);
            }
        }

        //Get currentUnit's index
        var availableUnitsArray = availableUnits.ToArray();
        int currentSelectedUnitsIndex = 0;
        for (int i = 0; i < availableUnits.Count; i++)
        {
            Unit tempUnit = availableUnitsArray[i];
            if (currentUnitSelected == tempUnit)
            {
                currentSelectedUnitsIndex = i;
            }
        }

        //Submit currentUnit + 1 index as new selection
        currentUnitSelected = availableUnitsArray[currentSelectedUnitsIndex + 1];
    }

    //Ation/Target Selection Data
    IDictionary<Unit, Ability> characterToAbilityDict = new Dictionary<Unit, Ability>();
    IDictionary<Unit, Unit> characterToTargetDict = new Dictionary<Unit, Unit>();

    private void Reset()
    {
        //Reset Character Selection To First Member
        //---Get list of possible party memebers to act
        List<Unit> availableUnits = new List<Unit>();
        foreach (Unit partyUnit in partyManager.partyUnits)
        {
            if (partyUnit.lifeStatus == Unit.LifeStatus.alive)
            {
                availableUnits.Add(partyUnit);
            }
        }
        currentUnitSelected = availableUnits.ToArray()[0];

        //Reset Ability/Target Selection
        characterToAbilityDict.Clear();
        characterToTargetDict.Clear();
    }

    public void SubmitCharacterAbilitySelection(GameObject abilityObject)
    {
        Unit character = currentUnitSelected;
        Ability ability = abilityObject.GetComponent<Ability>();

        characterToAbilityDict.Add(character, ability);
    }

    public void SubmitCharacterTargetSelection(GameObject targetObject)
    {
        Unit character = currentUnitSelected;
        Unit target = targetObject.GetComponent<Unit>();

        characterToTargetDict.Add(character, target);
    }

}
