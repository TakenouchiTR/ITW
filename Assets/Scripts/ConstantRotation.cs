using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationEulers;

    public Vector3 RotationEulers 
    { 
        get => this.rotationEulers;
        set => this.rotationEulers = value;
    }

    // Update is called once per frame
    void Update()
    {
        base.gameObject.transform.Rotate(RotationEulers * Time.deltaTime);
    }
}
