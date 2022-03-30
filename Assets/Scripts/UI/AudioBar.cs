using System;
using UnityEngine;

public class AudioBar : MonoBehaviour
{
    public event EventHandler PlayClicked;

    public void OnPlayClicked()
    {
        this.PlayClicked?.Invoke(this, EventArgs.Empty);
    }
}
