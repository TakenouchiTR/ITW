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
    public int PartCount => this.partStates.Count;

    /// <summary>
    ///     Gets the step count.
    /// </summary>
    /// <value>
    ///     The step count.
    /// </value>
    public int StepCount => this.titles.Count;

    /// <summary>
    ///     Gets the list of states for the current part.
    /// </summary>
    /// <value>
    ///     The list of states for the current part.
    /// </value>
    public List<PartState> CurrentPartStates => this.partStates[this.currentPartIndex];

    /// <summary>
    ///     Gets state for the current part at the current step.
    /// </summary>
    /// <value>
    ///     The state for the current part at the current step.
    /// </value>
    public PartState CurrentPartState => this.CurrentPartStates[this.currentStep];

    /// <summary>
    ///     Gets the title of the current state.
    /// </summary>
    /// <value>
    ///     The title of the current state.
    /// </value>
    public string CurrentTitle => this.titles[this.currentStep];

    /// <summary>
    ///     Gets the instruction text for the current step.
    /// </summary>
    /// <value>
    ///     The instruction text for the current step.
    /// </value>
    public string CurrentInstruction => this.instructions[this.currentStep];

    void Start()
    {
        this.StartNewFile();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            this.ChangeStep(this.currentStep - 1);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            this.ChangeStep(this.currentStep + 1);
        }
    }

    /// <summary>
    ///     Sets up the editor to work on a new file by resetting all fields to their default values.
    /// </summary>
    void StartNewFile()
    {
        this.fileLocation = null;
        this.titles = new List<string>();
        this.instructions = new List<string>();
        this.partStates = new List<List<PartState>>();

        this.currentStep = 0;
        this.currentPartIndex = 0;

        this.InsertNewStepBeforeCurrentStep();
        this.InsertNewPartBeforeCurrentPart();
        this.UpdateUI();
    }

    /// <summary>
    ///     Inserts the new part before current part.
    /// </summary>
    void InsertNewPartBeforeCurrentPart()
    {
        this.partStates.Insert(this.currentPartIndex, new List<PartState>());
        while (this.partStates[this.currentPartIndex].Count < this.StepCount)
        {
            this.partStates[this.currentPartIndex].Add(new PartState());
        }
    }

    /// <summary>
    ///     Inserts the new step before current step.
    /// </summary>
    void InsertNewStepBeforeCurrentStep()
    {
        this.titles.Insert(this.currentStep, "Title");
        this.instructions.Insert(this.currentStep, "Instructions");

        foreach (List<PartState> states in this.partStates)
        {
            states.Insert(this.currentStep, new PartState());
        }
    }

    /// <summary>
    ///     Inserts the new step after current step.
    /// </summary>
    void InsertNewStepAfterCurrentStep()
    {
        this.titles.Insert(this.currentStep + 1, "Title");
        this.instructions.Insert(this.currentStep + 1, "Instructions");

        foreach (List<PartState> states in this.partStates)
        {
            states.Insert(this.currentStep + 1, new PartState());
        }
    }

    /// <summary>
    /// Inserts the new part after current part.
    /// </summary>
    void InsertNewPartAfterCurrentPart()
    {
        this.partStates.Insert(this.currentPartIndex + 1, new List<PartState>());
        while (this.partStates[this.currentPartIndex + 1].Count < this.StepCount)
        {
            this.partStates[this.currentPartIndex + 1].Add(new PartState());
        }
    }

    /// <summary>
    ///     Redraws all of the UI elements to match the current values of the editor.
    /// </summary>
    void UpdateUI()
    {
        this.txt_TotalParts.text = "/ " + this.PartCount;
        this.txt_TotalSteps.text = "/ " + this.StepCount;
        this.inp_CurrentPart.text = (this.currentPartIndex + 1).ToString();
        this.inp_CurrentStep.text = (this.currentStep + 1).ToString();
        this.inp_Title.text = this.CurrentTitle;
        this.inp_Instructions.text = this.CurrentInstruction;
        this.inp_PosX.text = this.CurrentPartState.Position.x.ToString();
        this.inp_PosY.text = this.CurrentPartState.Position.y.ToString();
        this.inp_PosZ.text = this.CurrentPartState.Position.z.ToString();
    }

    /// <summary>
    ///     Changes the current step, updated the UI in the process.
    /// </summary>
    /// <param name="step">The new step.</param>
    void ChangeStep(int step)
    {
        if (step >= 0 && step < this.StepCount)
        {
            this.currentStep = step;
            this.UpdateUI();
        }
    }

    /// <summary>
    ///     Changes the current part, updating the UI in the process.
    /// </summary>
    /// <param name="part">The part index.</param>
    void ChangePart(int part)
    {
        if (part >= 0 && part < this.PartCount)
        {
            this.currentPartIndex = part;
            this.UpdateUI();
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
        if (this.StepCount > 1 && index >= 0 && index < this.StepCount)
        {
            this.titles.RemoveAt(index);
            this.instructions.RemoveAt(index);

            if (this.currentStep >= this.StepCount)
            {
                this.currentStep = this.StepCount - 1;
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
        if (this.PartCount > 1 && index >= 0 && index < this.PartCount)
        {
            this.partStates.RemoveAt(index);
        }

        if (this.currentPartIndex >= this.PartCount)
        {
            this.currentPartIndex = this.PartCount - 1;
        }
    }

    /// <summary>
    ///     Creates a file-writable save data object from the current values of the editor.
    /// </summary>
    /// <returns>A <see cref="TutorialData"/> object that may be saved to a file.</returns>
    TutorialData CreateSaveData()
    {
        PartTimeline[] states = new PartTimeline[this.PartCount];
        for (int i = 0; i < this.PartCount; i++)
        {
            states[i] = new PartTimeline(this.partStates[i].ToArray());
        }

        TutorialData data = new TutorialData()
        {
            Titles = this.titles.ToArray(),
            Instructions = this.instructions.ToArray(),
            States = states
        };

        return data;
    }

    /// <summary>
    ///     Displays the file browser to select a save location. If one is selected, the tutorial will be saved.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/> for running the coroutine.</returns>
    IEnumerator SaveFileCoroutine()
    {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, null, null, "Select File");

        if (FileBrowser.Success)
        {
            this.fileLocation = FileBrowser.Result[0];
            TutorialWriter.WriteFile(this.fileLocation, this.CreateSaveData());
        }
    }

    /// <summary>
    ///     Displays the file browser to select a file to load. If one is selected, the file will be loaded.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/> for running the coroutine.</returns>
    IEnumerator LoadFileCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select File");

        if (FileBrowser.Success)
        {
            this.currentPartIndex = 0;
            this.currentStep = 0;
            this.fileLocation = FileBrowser.Result[0];
            TutorialData data = TutorialReader.ReadFile(this.fileLocation);

            this.titles = data.Titles.ToList();
            this.instructions = data.Instructions.ToList();
            this.partStates = new List<List<PartState>>();

            for (int i = 0; i < data.PartCount; i++)
            {
                this.partStates.Add(data.States[i].States.ToList());
            }
            this.UpdateUI();
        }
    }

    public void OnAddNewStepClicked()
    {
        this.InsertNewStepAfterCurrentStep();
        this.ChangeStep(this.currentStep + 1);
    }

    public void OnAddNewPartClicked()
    {
        this.InsertNewPartAfterCurrentPart();
        this.ChangePart(this.currentPartIndex + 1);
    }

    public void OnRemovePartClicked()
    {
        this.RemovePart(this.currentPartIndex);
        this.UpdateUI();
    }

    public void OnRemoveStepClicked()
    {
        this.RemoveStep(this.currentStep);
        this.UpdateUI();
    }

    public void OnInsertStepClicked()
    {
        this.InsertNewStepBeforeCurrentStep();
        this.UpdateUI();
    }

    public void OnInsertPartClicked()
    {
        this.InsertNewPartBeforeCurrentPart();
        this.UpdateUI();
    }

    public void OnSaveClicked()
    {
        if (this.fileLocation == null)
        {
            this.StartCoroutine(this.SaveFileCoroutine());
        }
        else
        {
            TutorialWriter.WriteFile(this.fileLocation, this.CreateSaveData());
        }
    }

    public void OnSaveAsClicked()
    {
        this.StartCoroutine(this.SaveFileCoroutine());
    }

    public void OnLoadClicked()
    {
        this.StartCoroutine(this.LoadFileCoroutine());
    }

    public void OnCopyPositionForwardClicked()
    {
        if (this.currentStep < this.StepCount - 1)
        {
            this.CurrentPartStates[this.currentStep + 1].Position = this.CurrentPartState.Position;
        }
    }

    public void OnNewClicked()
    {
        this.StartNewFile();
    }

    public void OnStepEditEnd(string text)
    {
        if (int.TryParse(text, out int index))
        {
            this.ChangeStep(index - 1);
        }
    }

    public void OnPartEditEnd(string text)
    {
        if (int.TryParse(text, out int index))
        {
            this.ChangePart(index - 1);
        }

        this.UpdateUI();
    }

    public void OnTitleEditEnd(string text)
    {
        this.titles[this.currentStep] = text;
    }

    public void OnInstructionsEditEnd(string text)
    {
        this.instructions[this.currentStep] = text;
    }

    public void OnPositionEndEdit()
    {
        float x = float.Parse(this.inp_PosX.text);
        float y = float.Parse(this.inp_PosY.text);
        float z = float.Parse(this.inp_PosZ.text);
        this.CurrentPartState.Position = new Vector3(x, y, z);
    }
}
