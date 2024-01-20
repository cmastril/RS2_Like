using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandle : MonoBehaviour
{
    //References
    [SerializeField] private Menu menu;
    [SerializeField] private GameObject contentObject;

    private void Start()
    {
        //Add Listeners
        menu.cursorMoved.AddListener(MoveCursorToCursorLocation);
    }

    private void MoveCursorToCursorLocation()
    {
        RectTransform entryRect = contentObject.GetComponentsInChildren<MenuEntry>()[menu.cursorLocation].GetComponent<RectTransform>();
        this.GetComponent<RectTransform>().anchoredPosition = new Vector2(this.GetComponent<RectTransform>().anchoredPosition.x, entryRect.anchoredPosition.y);
    }

}
