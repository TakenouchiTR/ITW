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
    private const float ArrowAppearDelay = 5;

    private bool isActive = false;
    private int curStep = 0;
    private Vector3 moveLocation;
    private Vector3 startLocation;
    private Arrow arrow;
    private float arrowAppearTimer;

    [SerializeField]
    private Arrow arrowPrefab;

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
        get => this.isActive;
        private set
        {               
            this.isActive = value;
            base.GetComponent<Renderer>().material.SetFloat("_Intensity", this.isActive ? 1 : 0);

        }
    }

    public PartTimeline Steps { get; set; } = new PartTimeline();

    /// <summary>
    ///     Initializes this instance, setting its start location and initial move location.<br />
    ///     Must be run after the <c>Awake</c> step, but cannot be used in this instance's <c>Start</c> step.<br />
    ///     Should be run by the <see cref="StepController"/> that is holding this object in its <c>Start</c> step.
    /// </summary>
    public void Initialize()
    {
        this.startLocation = transform.position;
        this.moveLocation = this.startLocation;
        this.arrowAppearTimer = ArrowAppearDelay;

        if (this.arrowPrefab != null)
        {
            Arrow arrow = Instantiate(this.arrowPrefab);
            this.arrow = arrow;
            arrow.Target = base.gameObject;
            arrow.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            float step = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, this.moveLocation, step);
            this.IsMoving = transform.position != moveLocation;

            if (!this.IsMoving && this.curStep < this.Steps.Count - 1)
            {
                this.IsActive = this.moveLocation != this.startLocation + this.Steps[this.curStep + 1].Position;
                this.moveLocation = this.Steps[this.curStep + 1].Position;
            }
        }
        else if (this.IsActive && this.arrow != null && this.arrowAppearTimer > 0)
        {
            this.arrowAppearTimer -= Time.deltaTime;
            if (arrowAppearTimer <= 0)
            {
                this.arrow.gameObject.SetActive(true);
            }
        }
    }

    void OnEnable()
    {
        if (this.arrow != null && this.arrowAppearTimer <= 0)
        {
            this.arrow.gameObject.SetActive(true);
        }
    }

    void OnDisable()
    {
        if (this.arrow != null)
        {
            this.arrow.gameObject.SetActive(false);
        }
    }

    /// <summary>
    ///     Handles behaviour when interacted with, usually when clicked on with the mouse.
    /// </summary>
    public void Interact()
    {
        if (!this.IsActive || this.IsMoving)
            return;

        this.IsActive = false;
        this.IsMoving = true;
        this.moveLocation = this.curStep < this.Steps.Count - 1 ? this.startLocation + this.Steps[this.curStep + 1].Position : this.moveLocation;
        this.ActionCompleted?.Invoke(this, EventArgs.Empty);
        this.ResetArrow();
    }

    /// <summary>
    ///     Goes to the specified step, playing any animations to reach it.
    /// </summary>
    /// <param name="step">The step.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">step</exception>
    public void GotoStep(int step)
    {
        if (step < 0 || step >= this.Steps.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(step));
        }

        Vector3 newPosition = this.Steps[step].Position;

        //Moves if the part isn't in place for the step
        this.IsMoving = this.moveLocation != this.startLocation + newPosition;

        //Sets the next move location
        this.moveLocation = this.startLocation + newPosition;

        //Makes the part active if it is not at where it needs to be in the NEXT step
        this.IsActive = step < this.Steps.Count - 1 && this.Steps[step + 1].Position != newPosition;
        this.curStep = step;

        this.ResetArrow();
    }

    /// <summary>
    ///     Goes to the specified step instantly without playing any animations.
    /// </summary>
    /// <param name="step">The step.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">step</exception>
    public void GotoStepInstantly(int step)
    {
        if (step < 0 || step >= this.Steps.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(step));
        }

        Vector3 newPosition = this.Steps[step].Position;
        transform.position = startLocation + newPosition;

        this.IsActive = step < this.Steps.Count - 1 && this.Steps[step + 1].Position != newPosition;
        this.IsMoving = false;
        this.curStep = step;

        this.ResetArrow();
    }

    private void ResetArrow()
    {
        if (this.arrow != null)
        {
            this.arrow.gameObject.SetActive(false);
            this.arrowAppearTimer = ArrowAppearDelay;
        }
    }

    private void OnMouseDown()
    {
        if (!this.isActive || this.IsMoving || Cursor.lockState == CursorLockMode.Locked)
            return;

        this.Interact();
    }
}
