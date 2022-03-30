/// <summary>
///     Stores information about a given step, including the title, subtitle, and instructions.
/// </summary>
public class StepInformation
{
    /// <summary>
    ///     Gets or sets the title.
    /// </summary>
    /// <value>
    ///     The title.
    /// </value>
    public string Title { get; set; }

    /// <summary>
    ///     Gets or sets the subtitle.
    /// </summary>
    /// <value>
    ///     The subtitle.
    /// </value>
    public string Subtitle { get; set; }

    /// <summary>
    ///     Gets or sets the instruction.
    /// </summary>
    /// <value>
    ///     The instruction.
    /// </value>
    public string Instructions { get; set; }

    /// <summary>
    ///     Gets or sets the name of the audio file that should play when the step is loaded.
    /// </summary>
    /// <value>
    ///     The name of the audio file.
    /// </value>
    public string AudioFileName { get; set; }

    public bool HasAudioFile => !string.IsNullOrEmpty(this.AudioFileName);

    /// <summary>
    ///     Gets or sets the message to be displayed when the step is loaded.
    /// </summary>
    /// <value>
    ///     The message.
    /// </value>
    public Message Message { get; set; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StepInformation"/> class.
    /// </summary>
    public StepInformation()
    {
        this.Title = "";
        this.Subtitle = "";
        this.Instructions = "";
        this.AudioFileName = "";
        this.Message = new Message();
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="StepInformation"/> struct.<br/>
    ///     <c>titles</c> and <c>instructions</c> must have the same length.
    /// </summary>
    /// <param name="titles">The titles.</param>
    /// <param name="instructions">The instructions.</param>
    public StepInformation(string title, string subtitle, string instructions)
    {
        this.Title = title;
        this.Subtitle = subtitle;
        this.Instructions = instructions;
        this.AudioFileName = "";
        this.Message = new Message();
    }
}
