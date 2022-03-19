using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Represents an interactable part of the model.
/// </summary>
public class ModelPart : MonoBehaviour
{
    private const float MoveSpeed = 5;

    private bool isActive = false;
    private int curStep = 0;
    private Vector3 moveLocation;
    private Vector3 startLocation;

    /// <summary>
    ///     Occurs when [action completed].
    /// </summary>
    public event EventHandler ActionCompleted;

    /// <summary>
    ///     Gets a value indicating whether this instance is moving.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance is moving; otherwise, <c>false</c>.
    /// </value>
    public bool IsMoving { get; private set; }

    /// <summary>
    ///     Gets a value indicating whether this instance is active.<br />
    ///     While active, the part will have a glow to indicate to show that it can be interacted with.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    public bool IsActive
    {
        get => isActive;
        private set
        {               
            isActive = value;
            GetComponent<Renderer>().material.SetFloat("_Intensity", isActive ? 1 : 0);
        }
    }

    public List<PartState> Steps { get; set; } = new List<PartState>();

    /// <summary>
    ///     Initializes this instance, setting its start location and initial move location.<br />
    ///     Must be run after the <c>Awake</c> step, but cannot be used in this instance's <c>Start</c> step.<br />
    ///     Should be run by the <c>StepController</c> that is holding this object in its <c>Start</c> step.
    /// </summary>
    public void Initialize()
    {
        startLocation = transform.position;
        moveLocation = startLocation;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            float step = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveLocation, step);
            IsMoving = transform.position != moveLocation;

            if (!IsMoving && curStep < Steps.Count - 1)
            {
                IsActive = moveLocation != startLocation + Steps[curStep + 1].Position;
                moveLocation = Steps[curStep + 1].Position;
            }
        }
    }

    /// <summary>
    ///     Handles behaviour when interacted with, usually when clicked on with the mouse.
    /// </summary>
    public void Interact()
    {
        if (!IsActive || IsMoving)
            return;

        IsActive = false;
        IsMoving = true;
        moveLocation = curStep < Steps.Count - 1 ? startLocation + Steps[curStep + 1].Position : moveLocation;
        ActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    ///     Goes to the specified step, playing any animations to reach it.
    /// </summary>
    /// <param name="step">The step.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">step</exception>
    public void GotoStep(int step)
    {
        if (step < 0 || step >= Steps.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(step));
        }

        Vector3 newPosition = Steps[step].Position;

        //Moves if the part isn't in place for the step
        IsMoving = moveLocation != startLocation + newPosition;

        //Sets the next move location
        moveLocation = startLocation + newPosition;

        //Makes the part active if it is not at where it needs to be in the NEXT step
        IsActive = step < Steps.Count - 1 && Steps[step + 1].Position != newPosition;
        curStep = step;
    }

    /// <summary>
    ///     Goes to the specified step instantly without playing any animations.
    /// </summary>
    /// <param name="step">The step.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">step</exception>
    public void GotoStepInstantly(int step)
    {
        if (step < 0 || step >= Steps.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(step));
        }

        Vector3 newPosition = Steps[step].Position;
        transform.position = startLocation + newPosition;

        IsActive = step < Steps.Count - 1 && Steps[step + 1].Position != newPosition;
        IsMoving = false;
        curStep = step;
    }

    private void OnMouseDown()
    {
        if (!isActive || IsMoving)
            return;

        Interact();
    }
}
