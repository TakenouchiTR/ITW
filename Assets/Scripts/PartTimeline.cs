using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Stores a timeline of states for a part.
/// </summary>
public struct PartTimeline
{
    /// <summary>
    ///     Gets the part states.
    /// </summary>
    /// <value>
    ///     The part states.
    /// </value>
    public PartState[] States { get; set; }

    /// <summary>
    ///     Gets the count.
    /// </summary>
    /// <value>
    ///     The count.
    /// </value>
    public int Count => this.States.Length;
    
    /// <summary>
    ///     Gets or sets the <see cref="PartState"/> at the specified index.
    /// </summary>
    /// <value>
    ///     The <see cref="PartState"/>.
    /// </value>
    ///     <param name="index">The index.</param>
    /// <returns>The <see cref="PartState"/> at the specified index</returns>
    public PartState this[int index]
    {   
        get => this.States[index];
        set => this.States[index] = value;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PartTimeline"/> struct.
    /// </summary>
    /// <param name="states">The part states.</param>
    public PartTimeline(PartState[] states)
    {
        this.States = states;
    }
}
