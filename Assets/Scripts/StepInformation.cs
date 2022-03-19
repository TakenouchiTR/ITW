using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Stores information about a given step, including the title and instructions.
/// </summary>
public struct StepInformation
{
    /// <summary>
    ///     Gets or sets the titles.
    /// </summary>
    /// <value>
    ///     The titles.
    /// </value>
    public string[] Titles { get; set; }

    /// <summary>
    ///     Gets or sets the instructions.
    /// </summary>
    /// <value>
    ///     The instructions.
    /// </value>
    public string[] Instructions { get; set; }

    /// <summary>
    ///     Gets the total steps.
    /// </summary>
    /// <value>
    ///     The total steps.
    /// </value>
    public int TotalSteps => Titles.Length;

    /// <summary>
    ///     Initializes a new instance of the <see cref="StepInformation"/> struct.<br/>
    ///     <c>titles</c> and <c>instructions</c> must have the same length.
    /// </summary>
    /// <param name="titles">The titles.</param>
    /// <param name="instructions">The instructions.</param>
    /// <exception cref="System.ArgumentException">titles and instructions must have the same length</exception>
    public StepInformation(string[] titles, string[] instructions)
    {
        if (titles.Length != instructions.Length)
        {
            throw new ArgumentException("titles and instructions must have the same length");
        }

        this.Titles = titles;
        this.Instructions = instructions;
    }
}
