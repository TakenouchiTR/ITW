using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Draws a panel over the screen, dimming the world slightly.
/// </summary>
public class ScreenDimmer : MonoBehaviour
{
    /// <summary>
    ///     Dims the screen.
    /// </summary>
    public void DimScreen()
    {
        Animator animator = base.GetComponent<Animator>();
        animator.SetBool("Dim", true);
    }

    /// <summary>
    ///     Undims the screen.
    /// </summary>
    public void UndimScreen()
    {
        Animator animator = base.GetComponent<Animator>();
        animator.SetBool("Dim", false);
    }
}
