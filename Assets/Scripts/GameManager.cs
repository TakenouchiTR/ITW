using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     Manages the main game screen.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<StepController> stepControllers;

    [SerializeField]
    private List<GameObject> stepControllerPrefabs;

    [SerializeField]
    private TableOfContents tableOfContents;

    /// <summary>
    ///     Gets the currently active step controller.
    /// </summary>
    /// <value>
    ///     The current step controller.
    /// </value>
    public StepController CurrentStepController => this.stepControllers[this.stepControllers.Count - 1];

    void Start()
    {
        this.tableOfContents.SetTableContents(CurrentStepController.TableOfContentsEntries);
    }

    /// <summary>
    ///     Pushes a new <see cref="StepController"/> onto the stack using its prefab index, hiding the current active controller.
    /// </summary>
    /// <param name="prefabIndex">Index of the prefab.</param>
    public void PushController(int prefabIndex)
    {
        if (prefabIndex >= this.stepControllerPrefabs.Count || prefabIndex < 0)
        {
            Debug.LogError("prefabIndex out of range");
            return;
        }
        this.PushController(Instantiate(this.stepControllerPrefabs[prefabIndex].GetComponent<StepController>()));
    }

    /// <summary>
    ///     Pushes an already created <see cref="StepController"/> onto the stack, hiding the current active controller.
    /// </summary>
    /// <param name="controller">The controller.</param>
    public void PushController(StepController controller)
    {
        if (this.stepControllers.Contains(controller))
        {
            Debug.LogError("controller already added");
            return;
        }

        this.CurrentStepController.gameObject.SetActive(false);
        this.stepControllers.Add(controller);
        this.tableOfContents.SetTableContents(controller.TableOfContentsEntries);
    }

    /// <summary>
    ///     Pops the active controller from the top of the stack, destroying it and showing the next<br />
    ///     controller down the stack.
    /// </summary>
    public void PopController()
    {
        if (this.stepControllers.Count <= 1)
        {
            Debug.LogError("can't remove last prefab controller");
            return;
        }

        StepController prevStepController = this.CurrentStepController;
        this.stepControllers.RemoveAt(this.stepControllers.Count - 1);
        this.CurrentStepController.gameObject.SetActive(true);
        Destroy(prevStepController.gameObject);
        this.tableOfContents.SetTableContents(CurrentStepController.TableOfContentsEntries);
    }
    
    public void OnNextClicked()
    {
        this.CurrentStepController.GotoNextStep();
    }

    public void OnPrevClicked()
    {
        this.CurrentStepController.GotoPrevStep();
    }

    public void OnTableOfContentsClicked()
    {

    }

    public void OnClickableTextLinkedClicked(LinkCommand e)
    {
        int stepNumber;
        switch (e.Type)
        {
            case LinkCommandType.JUMP:
                stepNumber = int.Parse(e.Data);
                this.CurrentStepController.GotoStep(stepNumber);
                break;

            case LinkCommandType.IJMP:
                stepNumber = int.Parse(e.Data);
                this.CurrentStepController.GotoStepInstantly(stepNumber);
                break;

            case LinkCommandType.STUT:
                string[] subData = e.Data.Split(',');
                int prefabIndex = int.Parse(subData[0]);
                int subStep = int.Parse(subData[1]);

                this.PushController(prefabIndex);
                this.CurrentStepController.GotoStepInstantly(subStep);
                break;

            case LinkCommandType.RTRN:
                int returnStep = int.Parse(e.Data);
                this.PopController();

                if (returnStep == -1)
                {
                    returnStep = this.CurrentStepController.CurrentStep;
                }
                this.CurrentStepController.GotoStepInstantly(returnStep);
                break;

            default:
                Debug.LogError($"Unknown command ({e.Type})");
                break;
        }
    }

}
