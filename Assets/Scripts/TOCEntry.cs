using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Stores information about an entry for the table of contents.
/// </summary>
public struct TOCEntry
{
    /// <summary>
    ///     Gets or sets the text to show on the table of contents.
    /// </summary>
    /// <value>
    ///     The text.
    /// </value>
    public string Text { get; set; }

    /// <summary>
    ///     Gets or sets the step for the instruction.
    /// </summary>
    /// <value>
    ///     The index.
    /// </value>
    public int Index { get; set; }

    public TOCEntry(string text, int index)
    {
        this.Text = text;
        this.Index = index;
    }

    public override string ToString()
    {
        return $"{{Text:{this.Text}; Index: {this.Index}}}";
    }
}
