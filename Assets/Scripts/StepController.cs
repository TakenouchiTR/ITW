using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts;
using Assets.Scripts.IO;

/// <summary>
///     Controls the text and animamtions of a tutorial.
/// </summary>
public class StepController : MonoBehaviour
{
    private int curStep;
    private int totalSteps;
    private int totalActions;
    private int actionsRemaining;

    private string[] titles;
    private string[] instructionTexts;

    private TextMeshProUGUI txt_title;
    private TextMeshProUGUI txt_instructions;
    private TextMeshProUGUI txt_actionsRemaining;

    [SerializeField]
    private ModelPart[] parts;

    [SerializeField]
    private string filePath;

    /// <summary>
    ///     Gets a value indicating whether this instance can start the next step.<br />
    ///     An instance is able to start its next step iff all parts are not moving.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance can start its next step; otherwise, <c>false</c>.
    /// </value>
    private bool CanStartStep => !parts.Any(p => p.IsMoving);

    private void Awake()
    {
        txt_title = GameObject.FindGameObjectWithTag("Title").GetComponent<TextMeshProUGUI>();
        txt_instructions = GameObject.FindGameObjectWithTag("Instructions").GetComponent<TextMeshProUGUI>();
        txt_actionsRemaining = GameObject.FindGameObjectWithTag("ActionsRemaining").GetComponent<TextMeshProUGUI>();
        foreach (var part in parts)
        {
            part.ActionCompleted += OnActionComplete;
            part.Initialize();
        }
        LoadSteps();
        GotoStepInstantly(0);
    }

    /// <summary>
    ///     Updates the actions remaining display.
    /// </summary>
    private void UpdateActionsRemainingDisplay()
    {
        txt_actionsRemaining.text = $"Actions Left: {actionsRemaining}/{totalActions}";
    }

    /// <summary>
    ///     Loads the steps from a file. The file must be specified through the editor using<br />
    ///     the <c>filePath</c> field.
    /// </summary>
    private void LoadSteps()
    {
        TutorialData data = TutorialReader.ReadFile(filePath);

        titles = data.Titles;
        instructionTexts = data.Instructions;
        totalSteps = titles.Length;

        for (int i = 0; i < parts.Length; i++)
        {
            parts[i].Steps = data.States[i].ToList();
        }
    }

    /// <summary>
    ///     Goes to a specified step, having the parts play any animations.
    /// </summary>
    /// <param name="step">The step.</param>
    public void GotoStep(int step)
    {
        if (!CanStartStep)
            return;

        curStep = step;

        txt_title.text = titles[step];
        txt_instructions.text = instructionTexts[step];

        foreach (var part in parts)
        {
            part.GotoStep(step);
        }
    }

    /// <summary>
    ///     Goes to a specified step, without having the parts play any animations.
    /// </summary>
    /// <param name="step">The specified step.</param>
    public void GotoStepInstantly(int step)
    {
        if (!CanStartStep)
            return;

        curStep = step;

        txt_title.text = titles[step];
        txt_instructions.text = instructionTexts[step];

        foreach (var part in parts)
        {
            part.GotoStepInstantly(step);
        }
    }

    /// <summary>
    ///     Goes to the next step, if current step isn't the final step.<br />
    ///     Allows parts to play their animations to go to the next step.
    /// </summary>
    public void GotoNextStep()
    {
        if (!CanStartStep)
            return;

        curStep = curStep < totalSteps - 1 ? curStep + 1 : curStep;
        GotoStep(curStep);
    }

    /// <summary>
    ///     Goes to the previous step, if current step isn't the first step.<br />
    ///     Allows parts to play their animations to go to the previous step.
    /// </summary>
    public void GotoPrevStep()
    {
        if (!CanStartStep)
            return;

        curStep = curStep == 0 ? 0 : curStep - 1;
        GotoStep(curStep);
    }

    public void OnActionComplete(object sender, EventArgs e)
    {
        actionsRemaining--;
        UpdateActionsRemainingDisplay();
    }
}
