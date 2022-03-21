using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
///     Displays the table of contents for a tutorial.
/// </summary>
/// <seealso cref="UnityEngine.MonoBehaviour" />
public class TableOfContents : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txt_tableOfContents;

    /// <summary>
    ///     Builds the table of contents from an array of <see cref="TOCEntry"/>.
    /// </summary>
    /// <param name="entries">The entries.</param>
    public void SetTableContents(TOCEntry[] entries)
    {
        this.txt_tableOfContents.text = "";
        foreach (TOCEntry entry in entries)
        {
            this.txt_tableOfContents.text += $"<link=\"IJMP {entry.Index}>{entry.Text}</link>\n";
        }
    }

    /// <summary>
    ///     Enters the screen.
    /// </summary>
    public void EnterScreen()
    {
        Animator animator = base.GetComponent<Animator>();
        animator.SetBool("Open", true);
    }

    /// <summary>
    ///     Exits the screen.
    /// </summary>
    public void ExitScreen()
    {
        Animator animator = base.GetComponent<Animator>();
        animator.SetBool("Open", false);
    }
}
