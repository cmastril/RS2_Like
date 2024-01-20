using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    public List<Unit> partyUnits = new List<Unit>();

    //Public Methods
    public void AddPartyUnit(Unit unit)
    {
        partyUnits.Add(unit);
    }

    public Unit GetUnitFromIndex(int index)
    {
        return partyUnits.ToArray()[index];
    }

}
