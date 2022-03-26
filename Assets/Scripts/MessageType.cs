/// <summary>
///     Describes the type of message that should appear with a certain step.
/// </summary>
public enum MessageType
{
    /// <summary>
    ///     A generic message to inform the user about general information that may not be<br />
    ///     covered in the tutorial directly.<br />
    ///     <br />
    ///     (ie: The formula for an equation)
    /// </summary>
    Message,

    /// <summary>
    ///     Something that is important for the user to to be aware of that is more important than<br />
    ///     a formula or simple reminder, but not as serious as a warning.<br />
    ///     <br />
    ///     (ie: Wearing a mask during a sanding section)
    /// </summary>
    Alert,

    /// <summary>
    ///     A very serious warning that normally deals with health and safety matters. These should not be<br />
    ///     ignored, and some additional notification should be used when the user comes to a step containing<br />
    ///     a warning.<br />
    ///     <br />
    ///     (ie: Informing the user about potenial immediate dangers of residual charge.)
    /// </summary>
    Warning,
}
