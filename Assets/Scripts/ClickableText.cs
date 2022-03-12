using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ClickableText : MonoBehaviour
{
    public UnityEvent<LinkCommand> LinkClicked;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var text = GetComponent<TextMeshProUGUI>();
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
