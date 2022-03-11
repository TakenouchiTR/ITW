using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableLight : MonoBehaviour
{
    static readonly Color DayColor = new Color(255, 244, 214);
    static readonly Color NightColor = new Color(6, 9, 41);

    Vector3 startingEulers;
    Light light;

    private void Start()
    {
        startingEulers = transform.rotation.eulerAngles;
        light = GetComponent<Light>();
    }

    public void Rotate(float degrees)
    {
        Vector3 rotation = startingEulers + Vector3.right * degrees;
        if (rotation.x > 180 || rotation.x < 0)
        {
            rotation.x *= -1;
            light.color = NightColor;
        }
        else
        {
            light.color = DayColor;
        }
        transform.rotation = Quaternion.Euler(rotation);
    }
}
