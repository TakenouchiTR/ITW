using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts;

public class StepController : MonoBehaviour
{
    int curStep;
    int totalSteps;
    int totalActions;
    int actionsRemaining;
    bool isActive = true;

    string[] titles;
    string[] instructionTexts;

    [SerializeField]
    TextMeshProUGUI title;

    [SerializeField]
    TextMeshProUGUI instructions;
    
    [SerializeField]
    TextMeshProUGUI txt_actionsRemaining;

    [SerializeField]
    ModelPart[] parts;

    bool CanStartStep => !parts.Any(p => p.IsMoving);

    private void Start()
    {
        FindObjectOfType<ClickableText>().LinkClicked += OnClickableTextLinkedClicked;
        foreach (var part in parts)
        {
            part.ActionCompleted += OnActionComplete;
        }
        totalSteps = 5;
        titles = new string[totalSteps];
        instructionTexts = new string[totalSteps];
        CreateDefaultSteps();
        StartStep(0);
    }

    private void OnClickableTextLinkedClicked(object sender, LinkCommand e)
    {
        if (!isActive)
            return;

        switch (e.Type)
        {
            case LinkCommandType.JUMP:
                int stepNumber = int.Parse(e.Data);
                GotoStep(stepNumber);
                break;
            case LinkCommandType.STUT:
                break;
        }
    }

    private void UpdateActionsRemainingDisplay()
    {
        txt_actionsRemaining.text = $"Actions Left: {actionsRemaining}/{totalActions}";
    }

    void CreateDefaultSteps()
    {
        parts[0].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[0].Steps.Add(new PartState() { Position = new Vector3(-4, 0, 0) });
        parts[0].Steps.Add(new PartState() { Position = new Vector3(-4, 0, 0) });
        parts[0].Steps.Add(new PartState() { Position = new Vector3(-4, 0, 0) });
        parts[0].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[1].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[1].Steps.Add(new PartState() { Position = new Vector3(4, 0, 0) });
        parts[1].Steps.Add(new PartState() { Position = new Vector3(4, 0, 0) });
        parts[1].Steps.Add(new PartState() { Position = new Vector3(4, 0, 0) });
        parts[1].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[2].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[2].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[2].Steps.Add(new PartState() { Position = new Vector3(-4, 0, 0) });
        parts[2].Steps.Add(new PartState() { Position = new Vector3(-4, 0, 0) });
        parts[2].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[3].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[3].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[3].Steps.Add(new PartState() { Position = new Vector3(4, 0, 0) });
        parts[3].Steps.Add(new PartState() { Position = new Vector3(4, 0, 0) });
        parts[3].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[4].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[4].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[4].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[4].Steps.Add(new PartState() { Position = new Vector3(0, 1, .5f) });
        parts[4].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, 1, .5f) });
        parts[5].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, 1, .5f) });
        parts[6].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[7].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[7].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[7].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[7].Steps.Add(new PartState() { Position = new Vector3(0, 1, .5f) });
        parts[7].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        parts[8].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[8].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[8].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });
        parts[8].Steps.Add(new PartState() { Position = new Vector3(0, 1, .5f) });
        parts[8].Steps.Add(new PartState() { Position = new Vector3(0, 0, 0) });

        titles[0] = "Remove the back doors";
        titles[1] = "Remove the front doors";
        titles[2] = "Remove the screws";
        titles[3] = "Attach all the parts";
        titles[4] = "All done";

        instructionTexts[0] = "Remove the back doors please.\n To jump to the last step, <link=\"JUMP 3\"><color=blue><u>click me.</u></color></link>";
        instructionTexts[1] = "Remove the front doors please";
        instructionTexts[2] = "Remove the screws please";
        instructionTexts[3] = "Attach all the parts please";
        instructionTexts[4] = "All done please";
    }

    public void StartStep(int step)
    {
        if (!CanStartStep)
            return;
        title.text = titles[step];
        instructions.text = instructionTexts[step];
        foreach (var part in parts)
        {
            part.GotoStep(step);
        }
    }
    
    public void GotoStep(int step)
    {
        if (!CanStartStep)
            return;
        curStep = step;
        StartStep(step);
    }

    public void OnNextClicked()
    {
        if (!CanStartStep)
            return;
        curStep = curStep < totalSteps - 1 ? curStep + 1 : curStep;
        StartStep(curStep);
    }

    public void OnPrevClicked()
    {
        if (!CanStartStep)
            return;
        curStep = curStep == 0 ? 0 : curStep - 1;
        StartStep(curStep);
    }
    
    public void OnActionComplete(object sender, EventArgs e)
    {
        actionsRemaining--;
        UpdateActionsRemainingDisplay();
    }
}
