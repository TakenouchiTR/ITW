using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    Vector3 startingEulers;

    private void Start()
    {
        startingEulers = transform.rotation.eulerAngles;
    }

    public void Rotate(float degrees)
    {
        Vector3 rotation = startingEulers + Vector3.right * degrees;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
