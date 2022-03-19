using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Manages the main game screen.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<StepController> stepControllers;

    [SerializeField]
    List<GameObject> stepControllerPrefabs;

    /// <summary>
    ///     Gets the currently active step controller.
    /// </summary>
    /// <value>
    ///     The current step controller.
    /// </value>
    public StepController CurrentStepController => stepControllers[stepControllers.Count - 1];


    /// <summary>
    ///     Pushes a new controller onto the stack using its prefab index, hiding the current active controller.
    /// </summary>
    /// <param name="prefabIndex">Index of the prefab.</param>
    public void PushController(int prefabIndex)
    {
        if (prefabIndex >= stepControllerPrefabs.Count || prefabIndex < 0)
        {
            Debug.LogError("prefabIndex out of range");
            return;
        }
        PushController(Instantiate(stepControllerPrefabs[prefabIndex].GetComponent<StepController>()));
    }


    /// <summary>
    ///     Pushes an already created controller onto the stack, hiding the current active controller.
    /// </summary>
    /// <param name="controller">The controller.</param>
    public void PushController(StepController controller)
    {
        if (stepControllers.Contains(controller))
        {
            Debug.LogError("controller already added");
            return;
        }

        CurrentStepController.gameObject.SetActive(false);
        stepControllers.Add(controller);
    }


    /// <summary>
    ///     Pops the active controller from the top of the stack, destroying it and showing the next<br />
    ///     controller down the stack.
    /// </summary>
    public void PopController()
    {
        if (stepControllers.Count <= 1)
        {
            Debug.LogError("can't remove last prefab controller");
            return;
        }

        StepController prevStepController = CurrentStepController;
        stepControllers.RemoveAt(stepControllers.Count - 1);
        CurrentStepController.gameObject.SetActive(true);
        Destroy(prevStepController.gameObject);
    }
    
    public void OnNextClicked()
    {
        CurrentStepController.GotoNextStep();
    }

    public void OnPrevClicked()
    {
        CurrentStepController.GotoPrevStep();
    }

    public void OnClickableTextLinkedClicked(LinkCommand e)
    {
        switch (e.Type)
        {
            case LinkCommandType.JUMP:
                int stepNumber = int.Parse(e.Data);
                CurrentStepController.GotoStep(stepNumber);
                break;

            case LinkCommandType.STUT:
                string[] subData = e.Data.Split(',');
                int prefabIndex = int.Parse(subData[0]);
                int subStep = int.Parse(subData[1]);

                PushController(prefabIndex);
                CurrentStepController.GotoStepInstantly(subStep);
                break;

            case LinkCommandType.RTRN:
                int returnStep = int.Parse(e.Data);
                PopController();

                if (returnStep != -1)
                {
                    CurrentStepController.GotoStepInstantly(returnStep);
                }
                
                break;

            default:
                Debug.LogError($"Unknown command ({e.Type})");
                break;
        }
    }

}
