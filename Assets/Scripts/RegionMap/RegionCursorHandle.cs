using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RegionCursorHandle : MonoBehaviour
{
    //Events
    public UnityEvent regionSelectionMoved = new UnityEvent();

    //References
    private InputManager inputManager;

    //Fields
    [SerializeField] private List<Location> locations = new List<Location>();
    [SerializeField] public Location currentLocation;

    private void Awake()
    {
        //Get References
        inputManager = Object.FindObjectOfType<InputManager>();

        //Set Listeners
        inputManager.rightButtonPressed.AddListener(MoveCursorRight);
        inputManager.leftButtonPressed.AddListener(MoveCursorLeft);
        inputManager.submissionButtonPressed.AddListener(LoadCurrentArea);

        //Initialization
        Initialization();
    }

    private void LoadCurrentArea()
    {
        string sceneName = currentLocation.locationSceneName;
        SceneManager.LoadScene(sceneName);
    }

    [SerializeField] private Location startLocation;
    private void Initialization()
    {
        SetLocation(startLocation);
    }

    private void MoveCursorRight()
    {
        Location nextLocation = GetNextLocationToRight();
        SetLocation(nextLocation);
    }

    private Location GetNextLocationToRight()
    {
        Location nextLocation = currentLocation;

        Location[] locationsArray = locations.ToArray();

        //Get current index of current location selected
        int currentLocationIndex = GetCurrentSelectionIndex();

        //Run through every location to the right of this one, and return the first one that's available
        for (int i = 0; i < locationsArray.Length; i++)
        {
            Location currentIterationLocation = locationsArray[i];

            if (currentIterationLocation.isUnlocked == true && currentLocationIndex < i)
            {
                return currentIterationLocation;
            }
            
        }

        return nextLocation;
    }

    private void MoveCursorLeft()
    {
        //Get next location to right
        Location nextLocation = GetNextLocationToLeft();
        SetLocation(nextLocation);
    }

    private Location GetNextLocationToLeft()
    {
        Location[] locationsArray = locations.ToArray();

        //Get current index of current location selected
        int currentLocationIndex = GetCurrentSelectionIndex();

        //Run through every location to the left of this one, and return the last one before we get back to original selection
        Location latestCurrentLocation = currentLocation;
        for (int i = 0; i < locationsArray.Length; i++)
        {
            Location currentIterationLocation = locationsArray[i];

            if (i == currentLocationIndex)
            {
                return latestCurrentLocation;
            }

            if (currentIterationLocation.isUnlocked == true)
            {
                latestCurrentLocation = currentIterationLocation;
            }
        }

        return latestCurrentLocation;
    }

    private int GetCurrentSelectionIndex()
    {
        Location[] locationsArray = locations.ToArray();

        int currentLocationIndex = 0;
        for (int i = 0; i < locationsArray.Length; i++)
        {
            Location currentIterationLocation = locationsArray[i];
            if (currentIterationLocation == currentLocation)
            {
                currentLocationIndex = i;
            }
        }

        return currentLocationIndex;
    }

    private void SetLocation(Location location)
    {
        currentLocation = location;

        regionSelectionMoved.Invoke();
    }

}
