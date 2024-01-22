using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorDisplay : MonoBehaviour
{
    //References
    private RegionCursorHandle regionCursorHandle;
    [SerializeField] private GameObject cursorObject;

    private void Start()
    {
        //Referneces
        regionCursorHandle = Object.FindObjectOfType<RegionCursorHandle>();

        //Listeners
        regionCursorHandle.regionSelectionMoved.AddListener(CursorMovedHandle);

        //Initialization
        Initialization();
    }

    private void Initialization()
    {
        Location newLocation = regionCursorHandle.currentLocation;

        if (newLocation == null) return;

        MoveCursorToLocation(newLocation);
    }

    private void CursorMovedHandle()
    {
        Location newLocation = regionCursorHandle.currentLocation;
        MoveCursorToLocation(newLocation);
    }

    private void MoveCursorToLocation(Location location)
    {
        cursorObject.transform.position = location.transform.position;
    }

}
