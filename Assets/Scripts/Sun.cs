using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    static readonly Color DayColor = new Color(255, 244, 214);
    static readonly Color NightColor = new Color(6, 9, 41);

    Vector3 startingEulers;
    Light lightComponent;

    private void Start()
    {
        startingEulers = transform.rotation.eulerAngles;
        lightComponent = GetComponent<Light>();
    }

    public void Rotate(float degrees)
    {
        Vector3 rotation = startingEulers + Vector3.right * degrees;
        if (rotation.x > 180 || rotation.x < 0)
        {
            rotation.x *= -1;
            lightComponent.color = NightColor;
        }
        else
        {
            lightComponent.color = DayColor;
        }
        transform.rotation = Quaternion.Euler(rotation);
    }
}
