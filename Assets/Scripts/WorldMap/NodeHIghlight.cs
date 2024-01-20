using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHIghlight : MonoBehaviour
{
    //References
    WorldCursorHandle worldCursorHandle;

    //Fields
    [SerializeField] private Color baseColor;
    [SerializeField] private Color highlightColor;

    private void Start()
    {
        //Get References
        worldCursorHandle = Object.FindObjectOfType<WorldCursorHandle>();

        //Add Listeners
        worldCursorHandle.selectionMoved.AddListener(HighlightHandle);
    }

    private void HighlightHandle()
    {
        if (worldCursorHandle.currentSelection == this.gameObject.GetComponent<WorldNode>())
        {
            Highlight();
        }
        else
        {
            DeHighlight();
        }
    }

    private void Highlight()
    {
        SpriteRenderer sRend = this.gameObject.GetComponent<SpriteRenderer>();
        sRend.color = highlightColor;
    }

    private void DeHighlight()
    {
        SpriteRenderer sRend = this.gameObject.GetComponent<SpriteRenderer>();
        sRend.color = baseColor;
    }

}
