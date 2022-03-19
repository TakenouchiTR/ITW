using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///     Text that fires an event when clicked.
/// </summary>
public class ClickableText : MonoBehaviour
{
    private TextMeshProUGUI text;

    /// <summary>
    ///     Occurs when a link is clicked. Contains information about the type of command for the link and<br />
    ///     the additional data needed for the command.
    /// </summary>
    public UnityEvent<LinkCommand> LinkClicked;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            if (linkIndex > -1)
            {
                TMP_LinkInfo linkInfo = text.textInfo.linkInfo[linkIndex];
                string linkId = linkInfo.GetLinkID();
                string linkCode = linkId.Substring(0, 4);
                string linkData = linkId.Substring(5);
                LinkClicked?.Invoke(new LinkCommand(linkCode, linkData));
            }
        }
    }
}
