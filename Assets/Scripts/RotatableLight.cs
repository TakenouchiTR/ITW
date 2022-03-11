using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableLight : MonoBehaviour
{
    Vector3 startingEulers;

    private void Start()
    {
        startingEulers = transform.rotation.eulerAngles;
    }

    public void Rotate(float degrees)
    {
        transform.rotation = Quaternion.Euler(startingEulers + Vector3.right * degrees);
    }
}
