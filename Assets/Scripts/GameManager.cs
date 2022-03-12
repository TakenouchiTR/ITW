using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<StepController> stepControllers;

    [SerializeField]
    List<GameObject> stepControllerPrefabs;

    public StepController CurrentStepController => stepControllers[stepControllers.Count - 1];

    public void Push(int prefabIndex)
    {
        if (prefabIndex >= stepControllerPrefabs.Count)
        {
            Debug.LogError("prefabIndex out of range");
            return;
        }
        Push(Instantiate(stepControllerPrefabs[prefabIndex].GetComponent<StepController>()));
    }

    public void Push(StepController controller)
    {
        if (stepControllers.Contains(controller))
        {
            Debug.LogError("controller already added");
            return;
        }

        CurrentStepController.gameObject.SetActive(false);
        stepControllers.Add(controller);
    }

    public void Pop()
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

                Push(prefabIndex);
                CurrentStepController.GotoStepInstantly(subStep);
                break;

            case LinkCommandType.RTRN:
                int returnStep = int.Parse(e.Data);
                Pop();

                CurrentStepController.GotoStepInstantly(returnStep == -1 ? CurrentStepController.CurrentStep : returnStep);
                break;
        }
    }

}
