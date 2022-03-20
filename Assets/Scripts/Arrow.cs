using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     The behavior for a bobbing, rotating arrow.
/// </summary>
public class Arrow : MonoBehaviour
{
    private Vector3 bobOffset;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float bobDistance = .15f;

    [SerializeField]
    private float bobRate = 2;

    [SerializeField]
    private float rotationRate = .25f;

    public GameObject Target { get; set; }

    // Update is called once per frame
    void Update()
    {
        this.Bob();
        this.Rotate();
    }

    private void Bob()
    {
        this.bobOffset.y = (Mathf.Sin(Time.time * this.bobRate) + 1) * this.bobDistance / 2;
        base.transform.position = this.Target.transform.position + this.bobOffset + this.offset;
    }

    private void Rotate()
    {
        base.transform.Rotate(Vector3.forward, Time.deltaTime * 360 * this.rotationRate);
    }
}
