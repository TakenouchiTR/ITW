using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniStepController : StepController
{
    protected override void CreateDefaultSteps()
    {
        totalSteps = 6;
        titles = new string[totalSteps];
        instructionTexts = new string[totalSteps];
        parts[0].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[0].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[0].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[0].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[0].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[0].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[1].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[1].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[1].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[1].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[1].Steps.Add(new PartState() { Position = new Vector3(0, .5f, 0) });
        parts[1].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[2].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[2].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[2].Steps.Add(new PartState() { Position = new Vector3(0, 1, 0) });
        parts[2].Steps.Add(new PartState() { Position = new Vector3(0, 1, 0) });
        parts[2].Steps.Add(new PartState() { Position = new Vector3(0, 1, 0) });
        parts[2].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[3].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[3].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[3].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[3].Steps.Add(new PartState() { Position = new Vector3(.5f, 0, 0) });
        parts[3].Steps.Add(new PartState() { Position = new Vector3(.5f, 0, 0) });
        parts[3].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[4].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[4].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[4].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[4].Steps.Add(new PartState() { Position = new Vector3(-.5f, 0, 0) });
        parts[4].Steps.Add(new PartState() { Position = new Vector3(-.5f, 0, 0) });
        parts[4].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, .25f, .25f) });
        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, .25f, .25f) });
        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, .25f, .25f) });
        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, .25f, .25f) });
        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, .25f, .25f) });
        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, .25f, .25f) });
        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, .25f, .25f) });
        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, .25f, .25f) });
        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        titles[0] = "Remove eyes";
        titles[1] = "Lift head";
        titles[2] = "Remove arms";
        titles[3] = "Lift torso";
        titles[4] = "Reattach everything";
        titles[5] = "Done";

        instructionTexts[0] = "Before removing the head, the eyes must be removed first.";
        instructionTexts[1] = "Now the head may be removed";
        instructionTexts[2] = "Please remove the arms so the torse can be removed";
        instructionTexts[3] = "Please move the torso";
        instructionTexts[4] = "Reattach all parts";
        instructionTexts[5] = "<link=\"RTRN -1\"><color=blue><u>click me</u></color></link> to return to the previous tutorial.";
    }
}
