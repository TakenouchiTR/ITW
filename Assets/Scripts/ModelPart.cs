using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelPart : MonoBehaviour
{
    const float MoveSpeed = 5;
    bool isActive = false;
    bool hasMoved = false;
    Vector3 moveLocation;
    Vector3 startLocation;

    [SerializeField]
    Vector3 moveOffset;

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

    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsMoving)
        {
            float step = MoveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, moveLocation, step);
            IsMoving = transform.position != moveLocation;
        }
    }

    void Activate()
    {
        IsActive = true;
    }

    public void Interact()
    {
        if (!isActive)
            return;

        moveLocation = hasMoved ? startLocation : startLocation + moveOffset;

        IsActive = false;
        IsMoving = true;
        hasMoved = !hasMoved;
        ActionCompleted?.Invoke(this, EventArgs.Empty);
    }

    private void OnMouseDown()
    {
        if (!isActive || IsMoving)
            return;

        Interact();
    }
}
