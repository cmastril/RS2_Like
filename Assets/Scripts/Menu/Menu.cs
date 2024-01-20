using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Menu : MonoBehaviour
{
    //Events
    public UnityEvent cursorMoved = new UnityEvent();

    //Parameters
    [SerializeField] private int viewportEntryCount = 4;

    //References
    public GameObject contentObject;
    [SerializeField] private GameObject viewportObject;
    private InputManager inputManager;
 
    //Data
    //TEMPORARILY ALL SERIALIZED FIELDS
    public int cursorLocation = 0;
    private float entrySpaceSize = 0;
    private int viewportLocation = 0;
    private int entryCount;

    private void Start()
    {
        //References
        inputManager = Object.FindObjectOfType<InputManager>();
    }

    //Initialize recquired fields
    public void Initialize()
    {
        //Add Listeners
        inputManager.upButtonPressed.AddListener(MoveCursorUp);
        inputManager.downButtonPressed.AddListener(MoveCursorDown);
        inputManager.submissionButtonPressed.AddListener(SubmitSelection);

        entryCount = contentObject.GetComponentsInChildren<MenuEntry>().Length;
        CalculateEntrySpaceSize();
        LoadEntriesIntoMenu();
        SetCursorToFirstEntry();
    }

    public void Deinitialize()
    {
        //Remove listeners
        inputManager.upButtonPressed.RemoveListener(MoveCursorUp);
        inputManager.downButtonPressed.RemoveListener(MoveCursorDown);
        inputManager.submissionButtonPressed.RemoveListener(SubmitSelection);
    }

    //TEMPORARY PLACEMENT
    private void LoadEntriesIntoMenu()
    {
        //Get entry objects
        List<GameObject> entryObjects = new List<GameObject>();
        foreach (MenuEntry entry in contentObject.GetComponentsInChildren<MenuEntry>())
        {
            entryObjects.Add(entry.gameObject);
        }

        //Set Positions
        float topMostPoint = viewportObject.GetComponent<RectTransform>().sizeDelta.y / 2;
        Vector2 firstEntryLocation = new Vector2(entryObjects.ToArray()[0].GetComponent<RectTransform>().anchoredPosition.x, topMostPoint - (entrySpaceSize / 2));
        for (int i = 0; i < entryObjects.Count; i++)
        {
            GameObject entryObject = entryObjects.ToArray()[i];
            entryObject.GetComponent<RectTransform>().anchoredPosition = firstEntryLocation + (-i * new Vector2(0, entrySpaceSize));
        }

    }

    //Abstract Funcitonality
    public abstract void SubmitSelection();

    //Public Functionality
    private void MoveCursorUp()
    {
        if (cursorLocation == 0) return;

        //Move entries
        if (cursorLocation == viewportLocation)
        {
            MoveEntriesUp();
        }

        //Move Cursor
        cursorLocation -= 1;

        //Call Listener
        cursorMoved.Invoke();

    }

    private void MoveCursorDown()
    {
        if (cursorLocation == entryCount - 1) return;

        //Move Cursor
        cursorLocation += 1;

        //Move entries
        if (cursorLocation == (viewportLocation + viewportEntryCount))
        {
            MoveEntriesDown();
        }

        //Call Listener
        cursorMoved.Invoke();

    }

    //Non-Accesbile Functions
    private void MoveEntriesUp()
    {
        if (cursorLocation == 0) return;

        viewportLocation -= 1;

        //Move entries
        contentObject.GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, entrySpaceSize);
    }

    private void MoveEntriesDown()
    {
        if (cursorLocation == (viewportEntryCount - 1) ) return;

        viewportLocation += 1;

        //Move entries
        contentObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, entrySpaceSize);
    }

    public void SetCursorToFirstEntry()
    {
        //Call Listener
        cursorMoved.Invoke();
    }

    //Calculations
    private void CalculateEntrySpaceSize()
    {
        Vector2 viewportSize = viewportObject.GetComponent<RectTransform>().sizeDelta;
        entrySpaceSize = viewportSize.y / viewportEntryCount;
    }

}
