using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;
using Assets.Scripts;
using Assets.Scripts.IO;

/// <summary>
///     Controls the text and animations of a tutorial.
/// </summary>
public class StepController : MonoBehaviour
{
    private int curStep;
    private int totalActions;
    private int actionsRemaining;

    private TutorialData tutorialData;

    private TextMeshProUGUI txt_title;
    private TextMeshProUGUI txt_instructions;
    private TextMeshProUGUI txt_actionsRemaining;

    [SerializeField]
    private ModelPart[] parts;

    [SerializeField]
    private string filePath;

    /// <summary>
    ///     Gets the current step.
    /// </summary>
    /// <value>
    ///     The current step.
    /// </value>
    public int CurrentStep
    {
        get => curStep;
        private set => curStep = value;
    }

    /// <summary>
    ///     Gets the table of contents entries.
    /// </summary>
    /// <value>
    ///     The table of contents entries.
    /// </value>
    public TOCEntry[] TableOfContentsEntries => this.tutorialData.GenerateTableOfContents();

    /// <summary>
    ///     Gets a value indicating whether this instance can start the next step.<br />
    ///     An instance is able to start its next step iff all parts are not moving.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance can start its next step; otherwise, <c>false</c>.
    /// </value>
    private bool CanStartStep => !this.parts.Any(p => p.IsMoving);

    private void Awake()
    {
        this.txt_title = GameObject.FindGameObjectWithTag("Title").GetComponent<TextMeshProUGUI>();
        this.txt_instructions = GameObject.FindGameObjectWithTag("Instructions").GetComponent<TextMeshProUGUI>();
        this.txt_actionsRemaining = GameObject.FindGameObjectWithTag("ActionsRemaining").GetComponent<TextMeshProUGUI>();
        foreach (var part in this.parts)
        {
            part.ActionCompleted += OnActionComplete;
            part.Initialize();
        }
        this.LoadSteps();
        this.GotoStepInstantly(0);
    }

    /// <summary>
    ///     Updates the actions remaining display.
    /// </summary>
    private void UpdateActionsRemainingDisplay()
    {
        this.txt_actionsRemaining.text = $"Actions Left: {this.actionsRemaining}/{this.totalActions}";
    }

    /// <summary>
    ///     Loads the steps from a file. The file must be specified through the editor using<br />
    ///     the <c>filePath</c> field.
    /// </summary>
    private void LoadSteps()
    {
        this.tutorialData = TutorialReader.ReadFile(this.filePath);
    }

    /// <summary>
    ///     Goes to a specified step, having the parts play animations in the process.
    /// </summary>
    /// <param name="step">The step.</param>
    public void GotoStep(int step)
    {
        if (!this.CanStartStep)
            return;

        this.CurrentStep = step;
        this.UpdateText(this.tutorialData.StepInformation[step]);

        foreach (var part in this.parts)
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
        if (!this.CanStartStep)
            return;

        this.CurrentStep = step;
        this.UpdateText(this.tutorialData.StepInformation[step]);

        foreach (var part in this.parts)
        {
            part.GotoStepInstantly(step);
        }
    }

    private void UpdateText(StepInformation information)
    {
        this.txt_title.text = information.Title;
        this.txt_instructions.text = information.Subtitle;
    }

    /// <summary>
    ///     Goes to the next step, if current step isn't the final step.<br />
    ///     Allows parts to play their animations to go to the next step.
    /// </summary>
    public void GotoNextStep()
    {
        if (!this.CanStartStep)
            return;

        this.curStep = this.curStep < this.tutorialData.StepCount - 1 ? this.curStep + 1 : curStep;
        this.GotoStep(this.curStep);
    }

    /// <summary>
    ///     Goes to the previous step, if current step isn't the first step.<br />
    ///     Allows parts to play their animations to go to the previous step.
    /// </summary>
    public void GotoPrevStep()
    {
        if (!this.CanStartStep)
            return;

        this.curStep = this.curStep == 0 ? 0 : this.curStep - 1;
        this.GotoStep(this.curStep);
    }

    public void OnActionComplete(object sender, EventArgs e)
    {
        this.actionsRemaining--;
        this.UpdateActionsRemainingDisplay();
    }
}
