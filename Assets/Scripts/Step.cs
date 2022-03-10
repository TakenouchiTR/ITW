using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Step
{
    public StepCommand[] Commands { get; private set; }
    public string Title { get; private set; }
    public string Instructions { get; private set; }

    public Step(string title, string instructions, StepCommand[] commands)
    {
        Title = title;
        Instructions = instructions;
        this.Commands = commands;
    }
}
