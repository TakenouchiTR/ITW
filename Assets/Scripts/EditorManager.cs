using Assets.Scripts.IO;
using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
///     Manages information for the file editor.
/// </summary>
public class EditorManager : MonoBehaviour
{
    private int currentPartIndex = 0;
    private int currentStep = 0;
    private string fileLocation;
    private List<string> titles;
    private List<string> instructions;
    private List<List<PartState>> partStates;

    [SerializeField]
    private TextMeshProUGUI txt_TotalSteps;
    [SerializeField]
    private TextMeshProUGUI txt_TotalParts;
    [SerializeField]
    private TMP_InputField inp_CurrentStep;
    [SerializeField]
    private TMP_InputField inp_CurrentPart;
    [SerializeField]
    private TMP_InputField inp_Title;
    [SerializeField]
    private TMP_InputField inp_Instructions;
    [SerializeField]
    private TMP_InputField inp_PosX;
    [SerializeField]
    private TMP_InputField inp_PosY;
    [SerializeField]
    private TMP_InputField inp_PosZ;

    /// <summary>
    ///     Gets the part count.
    /// </summary>
    /// <value>
    ///     The part count.
    /// </value>
    public int PartCount => partStates.Count;

    /// <summary>
    ///     Gets the step count.
    /// </summary>
    /// <value>
    ///     The step count.
    /// </value>
    public int StepCount => titles.Count;

    /// <summary>
    ///     Gets the list of states for the current part.
    /// </summary>
    /// <value>
    ///     The list of states for the current part.
    /// </value>
    public List<PartState> CurrentPartStates => partStates[currentPartIndex];

    /// <summary>
    ///     Gets state for the current part at the current step.
    /// </summary>
    /// <value>
    ///     The state for the current part at the current step.
    /// </value>
    public PartState CurrentPartState => CurrentPartStates[currentStep];

    /// <summary>
    ///     Gets the title of the current state.
    /// </summary>
    /// <value>
    ///     The title of the current state.
    /// </value>
    public string CurrentTitle => titles[currentStep];

    /// <summary>
    ///     Gets the instruction text for the current step.
    /// </summary>
    /// <value>
    ///     The instruction text for the current step.
    /// </value>
    public string CurrentInstruction => instructions[currentStep];

    void Start()
    {
        StartNewFile();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            ChangeStep(currentStep - 1);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            ChangeStep(currentStep + 1);
        }
    }

    /// <summary>
    ///     Sets up the editor to work on a new file by resetting all fields to their default values.
    /// </summary>
    void StartNewFile()
    {
        fileLocation = null;
        titles = new List<string>();
        instructions = new List<string>();
        partStates = new List<List<PartState>>();

        currentStep = 0;
        currentPartIndex = 0;

        InsertNewStepBeforeCurrentStep();
        InsertNewPartBeforeCurrentPart();
        UpdateUI();
    }

    /// <summary>
    ///     Inserts the new part before current part.
    /// </summary>
    void InsertNewPartBeforeCurrentPart()
    {
        partStates.Insert(currentPartIndex, new List<PartState>());
        while (partStates[currentPartIndex].Count < StepCount)
        {
            partStates[currentPartIndex].Add(new PartState());
        }
    }

    /// <summary>
    ///     Inserts the new step before current step.
    /// </summary>
    void InsertNewStepBeforeCurrentStep()
    {
        titles.Insert(currentStep, "Title");
        instructions.Insert(currentStep, "Instructions");

        foreach (List<PartState> states in partStates)
        {
            states.Insert(currentStep, new PartState());
        }
    }

    /// <summary>
    ///     Inserts the new step after current step.
    /// </summary>
    void InsertNewStepAfterCurrentStep()
    {
        titles.Insert(currentStep + 1, "Title");
        instructions.Insert(currentStep + 1, "Instructions");

        foreach (List<PartState> states in partStates)
        {
            states.Insert(currentStep + 1, new PartState());
        }
    }

    /// <summary>
    /// Inserts the new part after current part.
    /// </summary>
    void InsertNewPartAfterCurrentPart()
    {
        partStates.Insert(currentPartIndex + 1, new List<PartState>());
        while (partStates[currentPartIndex + 1].Count < StepCount)
        {
            partStates[currentPartIndex + 1].Add(new PartState());
        }
    }

    /// <summary>
    ///     Redraws all of the UI elements to match the current values of the editor.
    /// </summary>
    void UpdateUI()
    {
        txt_TotalParts.text = "/ " + PartCount;
        txt_TotalSteps.text = "/ " + StepCount;
        inp_CurrentPart.text = (currentPartIndex + 1).ToString();
        inp_CurrentStep.text = (currentStep + 1).ToString();
        inp_Title.text = CurrentTitle;
        inp_Instructions.text = CurrentInstruction;
        inp_PosX.text = CurrentPartState.Position.x.ToString();
        inp_PosY.text = CurrentPartState.Position.y.ToString();
        inp_PosZ.text = CurrentPartState.Position.z.ToString();
    }

    /// <summary>
    ///     Changes the current step, updated the UI in the process.
    /// </summary>
    /// <param name="step">The new step.</param>
    void ChangeStep(int step)
    {
        if (step >= 0 && step < StepCount)
        {
            currentStep = step;
            UpdateUI();
        }
    }

    /// <summary>
    ///     Changes the current part, updating the UI in the process.
    /// </summary>
    /// <param name="part">The part index.</param>
    void ChangePart(int part)
    {
        if (part >= 0 && part < PartCount)
        {
            currentPartIndex = part;
            UpdateUI();
        }
    }

    /// <summary>
    ///     Removes the step at the specified index. Does not allow the final step to be deleted.<br />
    ///     <br />
    ///     If removing the step causes the current step to go out of bounds for the list of steps, it<br />
    ///     will be set to the last step.
    /// </summary>
    /// <param name="index">The index to remove.</param>
    void RemoveStep(int index)
    {
        if (StepCount > 1 && index >= 0 && index < StepCount)
        {
            titles.RemoveAt(index);
            instructions.RemoveAt(index);

            if (currentStep >= StepCount)
            {
                currentStep = StepCount - 1;
            }
        }
    }

    /// <summary>
    ///     Removes the part at the specified index. Does not allow the final part to be deleted.<br />
    ///     <br />
    ///     If removing the part causes the current part index to go out of bounds for the list of parts, it <br />
    ///     will be set to the last part.
    /// </summary>
    /// <param name="index">The index.</param>
    void RemovePart(int index)
    {
        if (PartCount > 1 && index >= 0 && index < PartCount)
        {
            partStates.RemoveAt(index);
        }

        if (currentPartIndex >= PartCount)
        {
            currentPartIndex = PartCount - 1;
        }
    }

    /// <summary>
    ///     Creates a file-writable save data object from the current values of the editor.
    /// </summary>
    /// <returns>A TutorialData object that may be saved to a file.</returns>
    TutorialData CreateSaveData()
    {
        PartState[][] states = new PartState[PartCount][];
        for (int i = 0; i < PartCount; i++)
        {
            states[i] = partStates[i].ToArray();
        }

        TutorialData data = new TutorialData()
        {
            Titles = titles.ToArray(),
            Instructions = instructions.ToArray(),
            States = states
        };

        return data;
    }

    /// <summary>
    ///     Displays the file browser to select a save location. If one is selected, the tutorial will be saved.
    /// </summary>
    /// <returns>The IEnumerator for running the coroutine.</returns>
    IEnumerator SaveFileCoroutine()
    {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, null, null, "Select File");

        if (FileBrowser.Success)
        {
            fileLocation = FileBrowser.Result[0];
            TutorialWriter.WriteFile(fileLocation, CreateSaveData());
        }
    }

    /// <summary>
    ///     Displays the file browser to select a file to load. If one is selected, the file will be loaded.
    /// </summary>
    /// <returns>The IEnumerator for running the coroutine.</returns>
    IEnumerator LoadFileCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select File");

        if (FileBrowser.Success)
        {
            currentPartIndex = 0;
            currentStep = 0;
            fileLocation = FileBrowser.Result[0];
            TutorialData data = TutorialReader.ReadFile(fileLocation);

            titles = data.Titles.ToList();
            instructions = data.Instructions.ToList();
            partStates = new List<List<PartState>>();

            for (int i = 0; i < data.PartCount; i++)
            {
                partStates.Add(data.States[i].ToList());
            }
            UpdateUI();
        }
    }

    public void OnAddNewStepClicked()
    {
        InsertNewStepAfterCurrentStep();
        ChangeStep(currentStep + 1);
    }

    public void OnAddNewPartClicked()
    {
        InsertNewPartAfterCurrentPart();
        ChangePart(currentPartIndex + 1);
    }

    public void OnRemovePartClicked()
    {
        RemovePart(currentPartIndex);
        UpdateUI();
    }

    public void OnRemoveStepClicked()
    {
        RemoveStep(currentStep);
        UpdateUI();
    }

    public void OnInsertStepClicked()
    {
        InsertNewStepBeforeCurrentStep();
        UpdateUI();
    }

    public void OnInsertPartClicked()
    {
        InsertNewPartBeforeCurrentPart();
        UpdateUI();
    }

    public void OnSaveClicked()
    {
        if (fileLocation == null)
        {
            StartCoroutine(SaveFileCoroutine());
        }
        else
        {
            TutorialWriter.WriteFile(fileLocation, CreateSaveData());
        }
    }

    public void OnSaveAsClicked()
    {
        StartCoroutine(SaveFileCoroutine());
    }

    public void OnLoadClicked()
    {
        StartCoroutine(LoadFileCoroutine());
    }

    public void OnCopyPositionForwardClicked()
    {
        if (currentStep < StepCount - 1)
        {
            CurrentPartStates[currentStep + 1].Position = CurrentPartState.Position;
        }
    }

    public void OnNewClicked()
    {
        StartNewFile();
    }

    public void OnStepEditEnd(string text)
    {
        if (int.TryParse(text, out int index))
        {
            ChangeStep(index - 1);
        }
    }

    public void OnPartEditEnd(string text)
    {
        if (int.TryParse(text, out int index))
        {
            ChangePart(index - 1);
        }

        UpdateUI();
    }

    public void OnTitleEditEnd(string text)
    {
        titles[currentStep] = text;
    }

    public void OnInstructionsEditEnd(string text)
    {
        instructions[currentStep] = text;
    }

    public void OnPositionEndEdit()
    {
        float x = float.Parse(inp_PosX.text);
        float y = float.Parse(inp_PosY.text);
        float z = float.Parse(inp_PosZ.text);
        CurrentPartState.Position = new Vector3(x, y, z);
    }
}
