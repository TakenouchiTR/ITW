using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts;
using Assets.Scripts.IO;

public class StepController : MonoBehaviour
{
    protected int curStep;
    protected int totalSteps;
    protected int totalActions;
    protected int actionsRemaining;
    protected bool isActive = true;

    protected string[] titles;
    protected string[] instructionTexts;

    TextMeshProUGUI title;
    TextMeshProUGUI instructions;
    TextMeshProUGUI txt_actionsRemaining;

    [SerializeField]
    protected ModelPart[] parts;

    [SerializeField]
    string filePath;

    protected bool CanStartStep => !parts.Any(p => p.IsMoving);

    public int CurrentStep => curStep;

    private void Awake()
    {
        title = GameObject.FindGameObjectWithTag("Title").GetComponent<TextMeshProUGUI>();
        instructions = GameObject.FindGameObjectWithTag("Instructions").GetComponent<TextMeshProUGUI>();
        txt_actionsRemaining = GameObject.FindGameObjectWithTag("ActionsRemaining").GetComponent<TextMeshProUGUI>();
        foreach (var part in parts)
        {
            part.ActionCompleted += OnActionComplete;
        }
        LoadSteps();
        GotoStepInstantly(0);
    }

    private void UpdateActionsRemainingDisplay()
    {
        txt_actionsRemaining.text = $"Actions Left: {actionsRemaining}/{totalActions}";
    }

    protected virtual void LoadSteps()
    {
        TutorialData data = TutorialReader.ReadFile(filePath);

        titles = data.Titles;
        instructionTexts = data.Instructions;

        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].Steps = data.States[i].ToList();
        }
    }

    private void StartStep(int step)
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

    public void GotoStepInstantly(int step)
    {
        if (!CanStartStep)
            return;
        curStep = step;
        title.text = titles[step];
        instructions.text = instructionTexts[step];
        foreach (var part in parts)
        {
            part.GotoStepInstantly(step);
        }
    }
    public void GotoNextStep()
    {
        if (!CanStartStep)
            return;
        curStep = curStep < totalSteps - 1 ? curStep + 1 : curStep;
        StartStep(curStep);
    }

    public void GotoPrevStep()
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
