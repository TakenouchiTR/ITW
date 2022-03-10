using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StepCommand
{
    public int PartIndex { get; set; }
    public string Method { get; set; }

    public StepCommand(int partIndex, string method)
    {
        this.PartIndex = partIndex;
        this.Method = method;
    }
}
