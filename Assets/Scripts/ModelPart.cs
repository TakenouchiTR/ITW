using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPart : MonoBehaviour
{
    const float MoveSpeed = 5;
    bool isActive = false;
    int curStep = 0;
    Vector3 moveLocation;
    Vector3 startLocation;

    public event EventHandler ActionCompleted;

    public bool IsMoving { get; private set; }
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

    private void Awake()
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

    public void Interact()
    {
        if (!IsActive || IsMoving)
            return;

        IsActive = false;
        IsMoving = true;
        moveLocation = curStep < Steps.Count - 1 ? startLocation + Steps[curStep + 1].Position : moveLocation;
        ActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    public void GotoStep(int step)
    {
        if (step < 0 || step >= Steps.Count)
            return;

        Vector3 newPosition = Steps[step].Position;

        //Moves if the part isn't in place for the step
        IsMoving = moveLocation != startLocation + newPosition;

        //Sets the next move location
        moveLocation = startLocation + newPosition;

        //Makes the part active if it is not at where it needs to be in the NEXT step
        IsActive = step < Steps.Count - 1 && Steps[step + 1].Position != newPosition;
        curStep = step;
    }

    public void GotoStepInstantly(int step)
    {
        if (step < 0 || step >= Steps.Count)
            return;

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
