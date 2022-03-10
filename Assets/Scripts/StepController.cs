using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

public class StepController : MonoBehaviour
{
    Step[] steps;
    int stepIndex;
    int totalActions;
    int actionsRemaining;

    [SerializeField]
    TextMeshProUGUI title;

    [SerializeField]
    TextMeshProUGUI instructions;
    
    [SerializeField]
    TextMeshProUGUI txt_actionsRemaining;

    [SerializeField]
    ModelPart[] parts;

    private void Start()
    {
        foreach (var part in parts)
        {
            part.ActionCompleted += OnActionComplete;
        }
        CreateDefaultSteps();
        StartStep();
    }

    private void UpdateActionsRemainingDisplay()
    {
        txt_actionsRemaining.text = $"Actions Left: {actionsRemaining}/{totalActions}";
    }

    void CreateDefaultSteps()
    {
        Step step1 = new Step
        (
            "Remove the Back Doors",
            "This where the instructions for what to do for a given step go. You should follow these " +
                "instructions if you want to complete the step. Failure to follow the instructions " +
                "written at the bottom of the screen may result in a step not being completed. " +
                "Incomplete steps are not complete, which may cause issues when trying to complete the step.",
            new StepCommand[]
            {
                new StepCommand(0, "Activate"),
                new StepCommand(1, "Activate"),
            }
        );
        Step step1_1 = new Step
        (
            "Remove the Front Doors",
            "Now that the back doors have been removed, the front doors can be safely removed as well. Ipsum " +
                "Lorem and all that jazz",
            new StepCommand[]
            {
                new StepCommand(2, "Activate"),
                new StepCommand(3, "Activate"),
            }
        );

        Step step2 = new Step
        (
            "Re-attach the Doors",
            "Now that the doors have been removed, please re-attach them to the helicopter. The helicopter " +
                "cannot be flown if the doors are not attached. Therefore, the doors should be attached so that " +
                "the helicopter can be flown once again.",
            new StepCommand[]
            {
                new StepCommand(0, "Activate"),
                new StepCommand(1, "Activate"),
            }
        );

        Step step3 = new Step
        (
            "Re-remove the Left Door",
            "Please remove the left door once again. There may have been an issue in the previous step.",
            new StepCommand[]
            {
                new StepCommand(0, "Activate"),
            }
        );

        Step step4 = new Step
        (
            "Re-re-attach the Left Door",
            "Now that the left door has been removed again, it must be attached again. Please attach the door to the " +
                "helicopter again. Once it is attached, it will be on the helicopter.",
            new StepCommand[]
            {
                new StepCommand(0, "Activate"),
            }
        );

        Step step5 = new Step
        (
            "Remove Screws from Between Windshields",
            "Remove the five screws from the frame between the two windshields.",
            new StepCommand[]
            {
                new StepCommand(4, "Activate"),
                new StepCommand(5, "Activate"),
                new StepCommand(6, "Activate"),
                new StepCommand(7, "Activate"),
                new StepCommand(8, "Activate"),
            }
        );

        Step step6 = new Step
        (
            "Re-attach all parts",
            "Re-attach all the parts that are currently removed from the chassis.",
            new StepCommand[]
            {
                new StepCommand(4, "Activate"),
                new StepCommand(5, "Activate"),
                new StepCommand(6, "Activate"),
                new StepCommand(7, "Activate"),
                new StepCommand(8, "Activate"),
                new StepCommand(2, "Activate"),
                new StepCommand(3, "Activate"),
            }
        );

        Step finalStep = new Step
        (
            "Complete",
            "The Demo has been completed.",
            new StepCommand[]
            {
                
            }
        );

        steps = new Step[] {
            step1,
            step1_1,
            step2,
            step3,
            step4,
            step5,
            step6,
            finalStep,
        };
    }

    public void StartStep()
    {
        Debug.Log(stepIndex);
        if (stepIndex > steps.Length || parts.Any(part => part.IsMoving))
            return;

        foreach (var part in parts.Where(part => part.IsActive))
        {
            part.Interact();
        }

        if (stepIndex < steps.Length)
        {
            Step currentStep = steps[stepIndex];
            StepCommand[] commands = currentStep.Commands;

            title.text = currentStep.Title;
            instructions.text = currentStep.Instructions;

            foreach (var command in commands)
            {
                parts[command.PartIndex].Invoke(command.Method, 0);
            }
            stepIndex++;

            totalActions = commands.Length;
            actionsRemaining = commands.Length;
            UpdateActionsRemainingDisplay();
        }
    }
    
    public void OnActionComplete(object sender, EventArgs e)
    {
        actionsRemaining--;
        UpdateActionsRemainingDisplay();
    }
}
