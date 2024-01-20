using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Combat : Menu
{
    CombatManager combatManager;
    private void Awake()
    {
        //References
        combatManager = Object.FindObjectOfType<CombatManager>();
    }

    public override void SubmitSelection()
    {
        if (combatManager.state != CombatManager.State.player_abilitySelection) return;

        //Grab reference to current cursor entry
        MenuEntry menuEntry = contentObject.GetComponentsInChildren<MenuEntry>()[cursorLocation];

        //Send it to the combat manager
        combatManager.SubmitCharacterAbilitySelection(menuEntry.reference);

        //tell combat mangaer to go next phase
        combatManager.NextPhase();
    }
}
