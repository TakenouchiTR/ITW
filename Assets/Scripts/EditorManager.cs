using Assets.Scripts.IO;
using SimpleFileBrowser;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    int currentPartIndex = 0;
    int currentStepIndex = 0;
    string fileLocation;
    List<string> titles;
    List<string> instructions;
    List<List<PartState>> partStates;

    [SerializeField]
    TextMeshProUGUI txt_TotalSteps;
    [SerializeField]
    TextMeshProUGUI txt_TotalParts;
    [SerializeField]
    TMP_InputField inp_CurrentStep;
    [SerializeField]
    TMP_InputField inp_CurrentPart;
    [SerializeField]
    TMP_InputField inp_Title;
    [SerializeField]
    TMP_InputField inp_Instructions;
    [SerializeField]
    TMP_InputField inp_PosX;
    [SerializeField]
    TMP_InputField inp_PosY;
    [SerializeField]
    TMP_InputField inp_PosZ;


    public int PartCount => partStates.Count;
    public int StepCount => titles.Count;
    public List<PartState> CurrentPart => partStates[currentPartIndex];
    public PartState CurrentPartState => CurrentPart[currentStepIndex];
    public string CurrentTitle => titles[currentStepIndex];
    public string CurrentInstruction => instructions[currentStepIndex];

    void Start()
    {
        StartNewFile();
    }

    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            ChangeStep(currentStepIndex - 1);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            ChangeStep(currentStepIndex + 1);
        }
    }

    void StartNewFile()
    {
        fileLocation = null;
        titles = new List<string>();
        instructions = new List<string>();
        partStates = new List<List<PartState>>();

        currentStepIndex = 0;
        currentPartIndex = 0;

        InsertNewStepBeforeCurrentStep();
        InsertNewPartBeforeCurrentPart();
        UpdateUI();
    }

    void InsertNewPartBeforeCurrentPart()
    {
        partStates.Insert(currentPartIndex, new List<PartState>());
        while (partStates[currentPartIndex].Count < StepCount)
        {
            partStates[currentPartIndex].Add(new PartState());
        }
    }

    void InsertNewStepBeforeCurrentStep()
    {
        titles.Insert(currentStepIndex, "Title");
        instructions.Insert(currentStepIndex, "Instructions");

        foreach (List<PartState> states in partStates)
        {
            states.Insert(currentStepIndex, new PartState());
        }
    }

    void InsertNewStepAfterCurrentStep()
    {
        titles.Insert(currentStepIndex + 1, "Title");
        instructions.Insert(currentStepIndex + 1, "Instructions");

        foreach (List<PartState> states in partStates)
        {
            states.Insert(currentStepIndex + 1, new PartState());
        }
    }

    void InsertNewPartAfterCurrentPart()
    {
        partStates.Insert(currentPartIndex + 1, new List<PartState>());
        while (partStates[currentPartIndex + 1].Count < StepCount)
        {
            partStates[currentPartIndex + 1].Add(new PartState());
        }
    }

    void UpdateUI()
    {
        txt_TotalParts.text = "/ " + PartCount;
        txt_TotalSteps.text = "/ " + StepCount;
        inp_CurrentPart.text = (currentPartIndex + 1).ToString();
        inp_CurrentStep.text = (currentStepIndex + 1).ToString();
        inp_Title.text = CurrentTitle;
        inp_Instructions.text = CurrentInstruction;
        inp_PosX.text = CurrentPartState.Position.x.ToString();
        inp_PosY.text = CurrentPartState.Position.y.ToString();
        inp_PosZ.text = CurrentPartState.Position.z.ToString();
    }

    void ChangeStep(int step)
    {
        if (step >= 0 && step < StepCount)
        {
            currentStepIndex = step;
            UpdateUI();
        }
    }

    void ChangePart(int part)
    {
        if (part >= 0 && part < PartCount)
        {
            currentPartIndex = part;
            UpdateUI();
        }
    }

    void RemoveStep(int index)
    {
        if (StepCount > 1 && index >= 0 && index < StepCount)
        {
            titles.RemoveAt(index);
            instructions.RemoveAt(index);

            if (currentStepIndex >= StepCount)
            {
                currentStepIndex = StepCount - 1;
            }
        }
    }

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

    IEnumerator SaveFileCoroutine()
    {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files, false, null, null, "Select File");

        if (FileBrowser.Success)
        {
            fileLocation = FileBrowser.Result[0];
            TutorialWriter.WriteFile(fileLocation, CreateSaveData());
        }
    }

    IEnumerator LoadFileCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Select File");

        if (FileBrowser.Success)
        {
            currentPartIndex = 0;
            currentStepIndex = 0;
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
        ChangeStep(currentStepIndex + 1);
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
        RemoveStep(currentStepIndex);
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
        if (currentStepIndex < StepCount - 1)
        {
            CurrentPart[currentStepIndex + 1].Position = CurrentPartState.Position;
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
        titles[currentStepIndex] = text;
    }

    public void OnInstructionsEditEnd(string text)
    {
        instructions[currentStepIndex] = text;
    }

    public void OnPositionEndEdit()
    {
        float x = float.Parse(inp_PosX.text);
        float y = float.Parse(inp_PosY.text);
        float z = float.Parse(inp_PosZ.text);
        CurrentPartState.Position = new Vector3(x, y, z);
    }
}
